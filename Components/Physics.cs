using GP_Final_Catapult.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GP_Final_Catapult.Components {
	public class Physics : IComponent {
		public Vector2 Acceleration;
		public Vector2 Velocity;
		public float GRAVITY = 981;
		public float Restitution;
		public float Mass;

		public enum PhysicsType {
			STATICS,
			KINEMATICS,
			DYNAMICS
		}
		public PhysicsType EntityPhysicsType;

		public enum BoundingBoxType {
			NONE,
			AABB,
			CIRCLE
		}
		public BoundingBoxType EntityBoundingBoxType;

		public enum ImpluseType {
			NONE,
			SURFACE,
			NORMAL
		}
		public ImpluseType EntityImpluseType;

		public List<CollisionManifold> CollideeManifold;
		public struct CollisionManifold {
			public IGameObject Collidee;
			public float Penetration;
			public Vector2 Normal;
		}

		public Physics() {
			Mass = float.PositiveInfinity;
			Restitution = 0f;
			EntityPhysicsType = PhysicsType.STATICS;
			EntityBoundingBoxType = BoundingBoxType.AABB;
			EntityImpluseType = ImpluseType.NONE;
			CollideeManifold = new List<CollisionManifold>();
		}

		public void Update(GameTime gameTime, ref Vector2 position, IGameObject parent, List<IGameObject> gameObjects) {
			//Position calculation
			switch (EntityPhysicsType) {
				case PhysicsType.STATICS:
					// do not do anything, this entity ignores all physics laws
					break;
				case PhysicsType.KINEMATICS:
					// entity is affected by everything except gravity
					Velocity += Acceleration * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
					position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
					break;
				case PhysicsType.DYNAMICS:
					// entity is affected by everything including gravity
					Acceleration.Y += GRAVITY;
					Velocity += Acceleration * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
					position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
					break;
			}

			//Detect collision
			CollideeManifold.Clear();
			CollisionManifold manifold;
			for (int i = 0; i < gameObjects.Count; i++) {
				switch (EntityBoundingBoxType) {
					case BoundingBoxType.AABB:
						if (gameObjects[i].GetComponent<Physics>().EntityBoundingBoxType == BoundingBoxType.AABB) {
							if (IsTouchingAABBAABB(parent, gameObjects[i], out manifold)) {
								CollideeManifold.Add(manifold);
							}
						} else if (gameObjects[i].GetComponent<Physics>().EntityBoundingBoxType == BoundingBoxType.CIRCLE) {
							if (IsTouchingAABBCircle(parent, gameObjects[i], out manifold)) {
								CollideeManifold.Add(manifold);
							}
						}
						break;
					case BoundingBoxType.CIRCLE:
						if (gameObjects[i].GetComponent<Physics>().EntityBoundingBoxType == BoundingBoxType.AABB) {
							if (IsTouchingCircleAABB(parent, gameObjects[i], out manifold)) {
								CollideeManifold.Add(manifold);
							}
						} else if (gameObjects[i].GetComponent<Physics>().EntityBoundingBoxType == BoundingBoxType.CIRCLE) {
							if (IsTouchingCircleCircle(parent, gameObjects[i], out manifold)) {
								CollideeManifold.Add(manifold);
							}
						}
						break;
				}
			}

			//Resolve collision
			foreach (CollisionManifold c in CollideeManifold) {
				switch (EntityImpluseType) {
					case ImpluseType.SURFACE:
						if (c.Collidee.GetComponent<Physics>().EntityImpluseType == ImpluseType.SURFACE) {
							//Calculate the move back amount 
							float moveBackPenetrationA = Velocity.Length() * c.Penetration / ( Velocity + c.Collidee.GetComponent<Physics>().Velocity ).Length();
							float moveBackPenetrationB = c.Collidee.GetComponent<Physics>().Velocity.Length() * c.Penetration / ( Velocity + c.Collidee.GetComponent<Physics>().Velocity ).Length();

							if (Velocity.Length() != 0) parent.transform.position = parent.transform.position - Vector2.Normalize(Velocity) * moveBackPenetrationA;
							if (c.Collidee.GetComponent<Physics>().Velocity.Length() != 0) c.Collidee.transform.position = c.Collidee.transform.position - Vector2.Normalize(c.Collidee.GetComponent<Physics>().Velocity) * moveBackPenetrationB;

							//No move after collision
							Velocity = Vector2.Zero;
							c.Collidee.GetComponent<Physics>().Velocity = Vector2.Zero;
						} else if (c.Collidee.GetComponent<Physics>().EntityImpluseType == ImpluseType.NORMAL) {
							//Calculate the move back amount (only to dynamics one)
							c.Collidee.transform.position = c.Collidee.transform.position + c.Normal * c.Penetration;

							//Calculate the direction
							Vector2 reflectDirection = c.Collidee.GetComponent<Physics>().Velocity - 2 * Vector2.Dot(c.Collidee.GetComponent<Physics>().Velocity, c.Normal) * c.Normal;

							//Calculate the force
							reflectDirection += Velocity;

							//calculate restitution
							//clamp value to be between 0 and 1 first
							float collideeRestitution = MathHelper.Clamp(c.Collidee.GetComponent<Physics>().Restitution, 0, 1);

							c.Collidee.GetComponent<Physics>().Velocity = reflectDirection * collideeRestitution;
						}
						break;
					case ImpluseType.NORMAL:
						if (c.Collidee.GetComponent<Physics>().EntityImpluseType == ImpluseType.SURFACE) {
							//Calculate the move back amount (only to dynamics one)
							parent.transform.position = parent.transform.position - c.Normal * c.Penetration;

							//Calculate the direction
							Vector2 reflectDirection = Velocity - 2 * Vector2.Dot(Velocity, c.Normal) * c.Normal;

							//Calculate the force
							reflectDirection += c.Collidee.GetComponent<Physics>().Velocity;

							//calculate restitution
							//clamp value to be between 0 and 1 first
							float colliderRestitution = MathHelper.Clamp(Restitution, 0, 1);

							Velocity = reflectDirection * colliderRestitution;
						} else if (c.Collidee.GetComponent<Physics>().EntityImpluseType == ImpluseType.NORMAL) {
							float moveBackPenetrationA = Velocity.Length() * c.Penetration / ( Velocity + c.Collidee.GetComponent<Physics>().Velocity ).Length();
							float moveBackPenetrationB = c.Collidee.GetComponent<Physics>().Velocity.Length() * c.Penetration / ( Velocity + c.Collidee.GetComponent<Physics>().Velocity ).Length();

							if (Velocity.Length() != 0) parent.transform.position = parent.transform.position - Vector2.Normalize(Velocity) * moveBackPenetrationA;
							if (c.Collidee.GetComponent<Physics>().Velocity.Length() != 0) c.Collidee.transform.position = c.Collidee.transform.position - Vector2.Normalize(c.Collidee.GetComponent<Physics>().Velocity) * moveBackPenetrationB;

							// Calculate relative velocity
							Vector2 rv = c.Collidee.GetComponent<Physics>().Velocity - Velocity;

							// Calculate relative velocity in terms of the normal direction
							float velAlongNormal = Vector2.Dot(rv, c.Normal);

							// Do not resolve if velocities are separating
							if (velAlongNormal <= 0) {
								// Calculate restitution
								float e = Math.Min(Restitution, c.Collidee.GetComponent<Physics>().Restitution);

								// Calculate impulse scalar
								float j = -( 1 + e ) * velAlongNormal / 2.0f;

								// Apply impulse
								Vector2 impulse = j * c.Normal;
								Velocity -= impulse;
								c.Collidee.GetComponent<Physics>().Velocity += impulse;
							}
						}
						break;
				}
			}
		}
		public bool IsTouching(IGameObject s) {
			foreach (CollisionManifold c in CollideeManifold) {
				if (c.Collidee.Equals(s)) return true;
			}
			return false;
		}

		private bool IsTouchingAABBCircle(IGameObject parent, IGameObject s, out CollisionManifold manifold) {
			manifold = new CollisionManifold();

			Vector2 halfA = new Vector2(parent.Rectangle.Width / 2.0f, parent.Rectangle.Height / 2.0f);
			Vector2 halfB = new Vector2(s.Rectangle.Width / 2.0f, s.Rectangle.Height / 2.0f);


			//find the centre of each sprite's hit area (using .Center() will give a loss of precision)
			Vector2 centreA = new Vector2(parent.transform.position.X + parent.Rectangle.X + parent.Rectangle.Width / 2, parent.transform.position.Y + parent.Rectangle.Y + parent.Rectangle.Height / 2);
			Vector2 centreB = new Vector2(s.transform.position.X + s.Rectangle.X + s.Rectangle.Width / 2, s.transform.position.Y + s.Rectangle.Y + s.Rectangle.Height / 2);

			// Vector from A to B
			Vector2 n = centreB - centreA;

			// Closest point on A to center of B
			Vector2 closest = n;

			// Clamp point to edges of the AABB
			closest.X = MathHelper.Clamp(closest.X, -halfA.X, halfA.X);
			closest.Y = MathHelper.Clamp(closest.Y, -halfA.Y, halfA.Y);

			bool inside = false;

			// Circle is inside the AABB, so we need to clamp the circle's center
			// to the closest edge
			if (n == closest) {
				inside = true;

				// Find closest axis
				if (Math.Abs(n.X) > Math.Abs(n.Y)) {
					// Clamp to closest extent
					closest.X = closest.X > 0 ? halfA.X : -halfA.X;
				}
				// y axis is shorter
				else {
					// Clamp to closest extent
					closest.Y = closest.Y > 0 ? halfA.Y : -halfA.Y;
				}
			}

			Vector2 normal = n - closest;
			float d = normal.LengthSquared();
			float r = s.Rectangle.Width < s.Rectangle.Height ? s.Rectangle.Width / 2.0f : s.Rectangle.Height / 2.0f;

			// Early out of the radius is shorter than distance to closest point and
			// Circle not inside the AABB
			if (d > r * r && !inside)
				return false;

			// AABBs have collided, now compute manifold
			manifold.Collidee = s;

			// Avoided sqrt until we needed
			d = (float)Math.Sqrt(d);

			// Collision normal needs to be flipped to point outside if circle was
			// inside the AABB
			if (inside) {
				manifold.Normal = -Vector2.Normalize(normal);
				manifold.Penetration = r - d;
			} else {
				manifold.Normal = Vector2.Normalize(normal);
				manifold.Penetration = r - d;
			}
			return true;
		}

		private bool IsTouchingCircleCircle(IGameObject parent, IGameObject s, out CollisionManifold manifold) {
			manifold = new CollisionManifold();

			float radiusA = parent.Rectangle.Width < parent.Rectangle.Height ? parent.Rectangle.Width / 2.0f : parent.Rectangle.Height / 2.0f;
			float radiusB = s.Rectangle.Width < s.Rectangle.Height ? s.Rectangle.Width / 2.0f : s.Rectangle.Height / 2.0f;

			//find the centre of each sprite's hit area (using .Center() will give a loss of precision)
			Vector2 centreA = new Vector2(parent.transform.position.X + parent.Rectangle.X + parent.Rectangle.Width / 2, parent.transform.position.Y + parent.Rectangle.Y + parent.Rectangle.Height / 2);
			Vector2 centreB = new Vector2(s.transform.position.X + s.Rectangle.X + s.Rectangle.Width / 2, s.transform.position.Y + s.Rectangle.Y + s.Rectangle.Height / 2);

			float r = radiusA + radiusB;
			float r2 = r * r;
			float distanceSquared = Vector2.DistanceSquared(centreA, centreB);
			if (r2 < distanceSquared) {
				//Not collided
				return false;
			} else {
				// Circles have collided, now compute manifold
				manifold.Collidee = s;

				// Calculate distance
				float distance = (float)Math.Sqrt(distanceSquared);

				if (distance != 0) {
					// If distance between circles is not zero
					// Normal vector is points from A to B, and is a unit vector
					manifold.Normal = ( centreB - centreA ) / distance;

					// Distance is difference between radius and distance
					manifold.Penetration = r - distance;
				} else {
					// If distance between circles is zero
					manifold.Normal = Vector2.UnitX;

					// Distance is difference between radius and distance
					manifold.Penetration = (float)Math.Min(parent.Rectangle.Width / 2.0, s.Rectangle.Width / 2.0);

				}

				return true;
			}
		}

		private bool IsTouchingCircleAABB(IGameObject parent, IGameObject s, out CollisionManifold manifold) {
			manifold = new CollisionManifold();

			Vector2 halfA = new Vector2(parent.Rectangle.Width / 2.0f, parent.Rectangle.Height / 2.0f);
			Vector2 halfB = new Vector2(s.Rectangle.Width / 2.0f, s.Rectangle.Height / 2.0f);


			//find the centre of each sprite's hit area (using .Center() will give a loss of precision)
			Vector2 centreA = new Vector2(parent.transform.position.X + parent.Rectangle.X + parent.Rectangle.Width / 2, parent.transform.position.Y + parent.Rectangle.Y + parent.Rectangle.Height / 2);
			Vector2 centreB = new Vector2(s.transform.position.X + s.Rectangle.X + s.Rectangle.Width / 2, s.transform.position.Y + s.Rectangle.Y + s.Rectangle.Height / 2);

			// Vector from B to A
			Vector2 n = centreA - centreB;

			// Closest point on B to center of A
			Vector2 closest = n;

			// Clamp point to edges of the AABB
			closest.X = MathHelper.Clamp(closest.X, -halfB.X, halfB.X);
			closest.Y = MathHelper.Clamp(closest.Y, -halfB.Y, halfB.Y);

			bool inside = false;

			// Circle is inside the AABB, so we need to clamp the circle's center
			// to the closest edge
			if (n == closest) {
				inside = true;

				// Find closest axis
				if (Math.Abs(n.X) > Math.Abs(n.Y)) {
					// Clamp to closest extent
					closest.X = closest.X > 0 ? halfB.X : -halfB.X;
				}
				// y axis is shorter
				else {
					// Clamp to closest extent
					closest.Y = closest.Y > 0 ? halfB.Y : -halfB.Y;
				}
			}

			Vector2 normal = n - closest;
			float d = normal.LengthSquared();
			float r = parent.Rectangle.Width < parent.Rectangle.Height ? parent.Rectangle.Width / 2.0f : parent.Rectangle.Height / 2.0f;

			// Early out of the radius is shorter than distance to closest point and
			// Circle not inside the AABB
			if (d > r * r && !inside)
				return false;

			// AABBs have collided, now compute manifold
			manifold.Collidee = s;

			// Avoided sqrt until we needed
			d = (float)Math.Sqrt(d);

			// Collision normal needs to be flipped to point outside if circle was
			// inside the AABB
			if (inside) {
				manifold.Normal = Vector2.Normalize(normal);
				manifold.Penetration = r - d;
			} else {
				manifold.Normal = -Vector2.Normalize(normal);
				manifold.Penetration = r - d;
			}
			return true;
		}

		private bool IsTouchingAABBAABB(IGameObject parent, IGameObject s, out CollisionManifold manifold) {
			manifold = new CollisionManifold();

			Vector2 halfA = new Vector2(parent.Rectangle.Width / 2.0f, parent.Rectangle.Height / 2.0f);
			Vector2 halfB = new Vector2(s.Rectangle.Width / 2.0f, s.Rectangle.Height / 2.0f);

			//find the centre of each sprite's hit area (using .Center() will give a loss of precision)
			Vector2 centreA = new Vector2(parent.transform.position.X + parent.Rectangle.X + halfA.X, parent.transform.position.Y + parent.Rectangle.Y + halfA.Y);
			Vector2 centreB = new Vector2(s.transform.position.X + s.Rectangle.X + halfB.X, s.transform.position.Y + s.Rectangle.Y + halfB.Y);

			//Not collided
			if (parent.Rectangle.Right < s.Rectangle.Left || parent.Rectangle.Left > s.Rectangle.Right) return false;
			if (parent.Rectangle.Bottom < s.Rectangle.Top || parent.Rectangle.Top > s.Rectangle.Bottom) return false;

			// AABBs have collided, now compute manifold
			manifold.Collidee = s;

			float distance = Vector2.Distance(centreA, centreB);

			if (distance != 0) {
				// If distance between AABBs is not zero

				// Distance is difference between radius and distance
				float xOverlapAB = ( halfA.X + halfB.X ) - ( centreA.X - centreB.X );
				float xOverlapBA = ( halfA.X + halfB.X ) - ( centreB.X - centreA.X );
				float yOverlapAB = ( halfA.Y + halfB.Y ) - ( centreA.Y - centreB.Y );
				float yOverlapBA = ( halfA.Y + halfB.Y ) - ( centreB.Y - centreA.Y );

				manifold.Penetration = Math.Min(xOverlapAB, Math.Min(xOverlapBA, Math.Min(yOverlapAB, yOverlapBA)));

				//calculate normal vector
				if (manifold.Penetration == xOverlapAB) {
					//Left of A
					manifold.Normal = new Vector2(-1, 0);
				} else if (manifold.Penetration == xOverlapBA) {
					//Right of A
					manifold.Normal = new Vector2(1, 0);
				} else if (manifold.Penetration == yOverlapAB) {
					//Top of A
					manifold.Normal = new Vector2(0, -1);
				} else if (manifold.Penetration == yOverlapBA) {
					//Bottom of A
					manifold.Normal = new Vector2(0, 1);
				}
			} else {
				// If distance between circles is zero
				manifold.Normal = Vector2.UnitX;

				// Distance is difference between radius and distance
				manifold.Penetration = (float)Math.Min(parent.Rectangle.Width / 2.0, s.Rectangle.Width / 2.0);

			}

			return true;
		}

		private float CalculateTimeCollided(IGameObject A, IGameObject B) {
			return (float)( -1 / 2 * Math.Sqrt(
				Math.Pow(2 * A.transform.position.X * A.GetComponent<Physics>().Velocity.X - 2 * A.transform.position.X * B.GetComponent<Physics>().Velocity.X + 2 * A.transform.position.Y * A.GetComponent<Physics>().Velocity.Y - 2 * A.transform.position.Y * B.GetComponent<Physics>().Velocity.Y - 2 * B.transform.position.X * A.GetComponent<Physics>().Velocity.X + 2 * B.transform.position.X * B.GetComponent<Physics>().Velocity.X - 2 * B.transform.position.Y * A.GetComponent<Physics>().Velocity.Y + 2 * B.transform.position.Y * B.GetComponent<Physics>().Velocity.Y, 2) -
				4 * ( A.GetComponent<Physics>().Velocity.X * A.GetComponent<Physics>().Velocity.X - 2 * A.GetComponent<Physics>().Velocity.X * B.GetComponent<Physics>().Velocity.X + B.GetComponent<Physics>().Velocity.X * B.GetComponent<Physics>().Velocity.X + A.GetComponent<Physics>().Velocity.Y * A.GetComponent<Physics>().Velocity.Y - 2 * A.GetComponent<Physics>().Velocity.Y * B.GetComponent<Physics>().Velocity.Y + B.GetComponent<Physics>().Velocity.Y * B.GetComponent<Physics>().Velocity.Y ) *
				( A.transform.position.X * A.transform.position.X - 2 * A.transform.position.X * B.transform.position.X + A.transform.position.Y * A.transform.position.Y - 2 * A.transform.position.Y * B.transform.position.Y + B.transform.position.X * B.transform.position.X + B.transform.position.Y * B.transform.position.Y - A.Rectangle.X / 2 - 2 * A.Rectangle.X / 2 * B.Rectangle.X / 2 - B.Rectangle.X / 2 * B.Rectangle.X / 2 )) -
				A.transform.position.X * A.GetComponent<Physics>().Velocity.X + A.transform.position.X * B.GetComponent<Physics>().Velocity.X - A.transform.position.Y * A.GetComponent<Physics>().Velocity.Y + A.transform.position.Y * B.GetComponent<Physics>().Velocity.Y + B.transform.position.X * A.GetComponent<Physics>().Velocity.X - B.transform.position.X * B.GetComponent<Physics>().Velocity.X + B.transform.position.Y * A.GetComponent<Physics>().Velocity.Y - B.transform.position.Y * B.GetComponent<Physics>().Velocity.Y ) /
				( A.GetComponent<Physics>().Velocity.X * A.GetComponent<Physics>().Velocity.X - 2 * A.GetComponent<Physics>().Velocity.X * B.GetComponent<Physics>().Velocity.X + B.GetComponent<Physics>().Velocity.X * B.GetComponent<Physics>().Velocity.X + A.GetComponent<Physics>().Velocity.Y * A.GetComponent<Physics>().Velocity.Y - 2 * A.GetComponent<Physics>().Velocity.Y * B.GetComponent<Physics>().Velocity.Y + B.GetComponent<Physics>().Velocity.Y * B.GetComponent<Physics>().Velocity.Y );
		}
	}
}
