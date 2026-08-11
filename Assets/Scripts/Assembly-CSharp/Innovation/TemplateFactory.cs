using System;
using System.Collections.Generic;
using UnityEngine;

namespace Innovation
{
	public static class TemplateFactory
	{
		public static GameObject ApplyTemplate(GameObjectTemplate template, IResourceResolver resolver)
		{
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			return ApplyTemplate(null, template, resolver);
		}

		public static GameObject ApplyTemplate(GameObject gameObject, GameObjectTemplate template, IResourceResolver resolver)
		{
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			if (gameObject == null)
			{
				gameObject = new GameObject(template.Name);
			}
			gameObject.name = template.Name;
			gameObject.layer = template.Layer;
			gameObject.SetActive(template.Active);
			if (template.TransformTemplate != null)
			{
				ApplyTemplate(gameObject, template.TransformTemplate);
			}
			if (template.ColliderTemplate != null)
			{
				ApplyTemplate(gameObject, template.ColliderTemplate);
			}
			if (template.RendererTemplate != null)
			{
				ApplyTemplate(gameObject, template.RendererTemplate, resolver);
			}
			if (template.RigidbodyTemplate != null)
			{
				ApplyTemplate(gameObject, template.RigidbodyTemplate);
			}
			if (template.Children != null)
			{
				foreach (GameObjectTemplate child in template.Children)
				{
					ApplyTemplate(child, resolver).transform.parent = gameObject.transform;
				}
			}
			return gameObject;
		}

		public static Transform ApplyTemplate(GameObject gameObject, TransformTemplate template)
		{
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			Transform transform = gameObject.transform;
			transform.localPosition = template.LocalPosition;
			transform.localRotation = template.LocalRotation;
			transform.localScale = template.LocalScale;
			return transform;
		}

		public static Collider ApplyTemplate(GameObject gameObject, ColliderTemplate template)
		{
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			Collider collider;
			switch (template.Type)
			{
			case ColliderTypeCode.Box:
			{
				BoxCollider boxCollider = gameObject.AddOrGetComponent<BoxCollider>();
				boxCollider.center = template.Center;
				boxCollider.size = template.Size;
				collider = boxCollider;
				break;
			}
			case ColliderTypeCode.Sphere:
			{
				SphereCollider sphereCollider = gameObject.AddOrGetComponent<SphereCollider>();
				sphereCollider.center = template.Center;
				sphereCollider.radius = template.Radius;
				collider = sphereCollider;
				break;
			}
			case ColliderTypeCode.Capsule:
			{
				CapsuleCollider capsuleCollider = gameObject.AddOrGetComponent<CapsuleCollider>();
				capsuleCollider.center = template.Center;
				capsuleCollider.radius = template.Radius;
				capsuleCollider.height = template.Height;
				collider = capsuleCollider;
				break;
			}
			default:
				throw new ArgumentException("template");
			}
			collider.material.bounciness = template.Bounciness;
			collider.material.dynamicFriction = template.DynamicFriction;
			collider.material.staticFriction = template.StaticFriction;
			collider.material.bounceCombine = template.BounceCombine;
			collider.material.frictionCombine = template.FrictionCombine;
			return collider;
		}

		public static MeshRenderer ApplyTemplate(GameObject gameObject, RendererTemplate template, IResourceResolver resolver)
		{
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			MeshRenderer meshRenderer = gameObject.AddOrGetComponent<MeshRenderer>();
			MeshFilter meshFilter = gameObject.AddOrGetComponent<MeshFilter>();
			if (!string.IsNullOrEmpty(template.Shader))
			{
				if (resolver == null)
				{
					throw new ArgumentNullException("resolver");
				}
				meshRenderer.material.shader = resolver.ResolveShader(template.Shader);
			}
			if (!string.IsNullOrEmpty(template.Texture))
			{
				if (resolver == null)
				{
					throw new ArgumentNullException("resolver");
				}
				meshRenderer.material.mainTexture = resolver.ResolveTexture(template.Texture);
			}
			meshRenderer.material.color = (Color32)template.Color;
			meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Quad.fbx");
			return meshRenderer;
		}

		public static Rigidbody ApplyTemplate(GameObject gameObject, RigidbodyTemplate template)
		{
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			Rigidbody rigidbody = gameObject.AddOrGetComponent<Rigidbody>();
			rigidbody.mass = template.Mass;
			rigidbody.drag = template.Drag;
			rigidbody.angularDrag = template.AngularDrag;
			rigidbody.useGravity = template.UseGravity;
			rigidbody.isKinematic = template.IsKinematic;
			rigidbody.interpolation = template.Interpolation;
			rigidbody.collisionDetectionMode = template.CollisionDetectionMode;
			rigidbody.constraints = template.Constraints;
			return rigidbody;
		}

		public static GameObjectTemplate CreateTemplate(GameObject gameObject)
		{
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			GameObjectTemplate gameObjectTemplate = new GameObjectTemplate();
			gameObjectTemplate.Name = gameObject.name;
			Transform transform = gameObject.transform;
			Collider component = gameObject.GetComponent<Collider>();
			Renderer component2 = gameObject.GetComponent<Renderer>();
			Rigidbody component3 = gameObject.GetComponent<Rigidbody>();
			gameObjectTemplate.TransformTemplate = CreateTemplate(transform);
			if (component != null)
			{
				gameObjectTemplate.ColliderTemplate = CreateTemplate(component);
			}
			if (component2 != null)
			{
				gameObjectTemplate.RendererTemplate = CreateTemplate(component2);
			}
			if (component3 != null)
			{
				gameObjectTemplate.RigidbodyTemplate = CreateTemplate(component3);
			}
			int childCount = transform.childCount;
			gameObjectTemplate.Children = new List<GameObjectTemplate>(childCount);
			for (int i = 0; i < childCount; i++)
			{
				GameObjectTemplate item = CreateTemplate(transform.GetChild(i).gameObject);
				gameObjectTemplate.Children.Add(item);
			}
			return gameObjectTemplate;
		}

		public static TransformTemplate CreateTemplate(Transform transform)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			return new TransformTemplate
			{
				LocalPosition = transform.localPosition,
				LocalRotation = transform.localRotation,
				LocalScale = transform.localScale
			};
		}

		public static ColliderTemplate CreateTemplate(Collider collider)
		{
			if (collider == null)
			{
				throw new ArgumentNullException("collider");
			}
			ColliderTemplate colliderTemplate;
			if (collider is BoxCollider boxCollider)
			{
				colliderTemplate = new ColliderTemplate
				{
					Type = ColliderTypeCode.Box,
					Center = boxCollider.center,
					Size = boxCollider.size
				};
			}
			else if (collider is SphereCollider sphereCollider)
			{
				colliderTemplate = new ColliderTemplate
				{
					Type = ColliderTypeCode.Sphere,
					Center = sphereCollider.center,
					Radius = sphereCollider.radius
				};
			}
			else
			{
				if (!(collider is CapsuleCollider capsuleCollider))
				{
					throw new ArgumentException("collider");
				}
				colliderTemplate = new ColliderTemplate
				{
					Type = ColliderTypeCode.Capsule,
					Center = capsuleCollider.center,
					Radius = capsuleCollider.radius,
					Height = capsuleCollider.height
				};
			}
			colliderTemplate.Bounciness = collider.material.bounciness;
			colliderTemplate.DynamicFriction = collider.material.dynamicFriction;
			colliderTemplate.StaticFriction = collider.material.staticFriction;
			colliderTemplate.BounceCombine = collider.material.bounceCombine;
			colliderTemplate.FrictionCombine = collider.material.frictionCombine;
			return colliderTemplate;
		}

		public static RendererTemplate CreateTemplate(Renderer renderer)
		{
			if (renderer == null)
			{
				throw new ArgumentNullException("renderer");
			}
			return new RendererTemplate
			{
				Shader = renderer.material.shader.name,
				Texture = renderer.material.mainTexture?.name,
				Color = (HexColor)renderer.material.color
			};
		}

		public static RigidbodyTemplate CreateTemplate(Rigidbody rigidbody)
		{
			if (rigidbody == null)
			{
				throw new ArgumentNullException("rigidbody");
			}
			return new RigidbodyTemplate
			{
				Mass = rigidbody.mass,
				Drag = rigidbody.drag,
				AngularDrag = rigidbody.angularDrag,
				UseGravity = rigidbody.useGravity,
				IsKinematic = rigidbody.isKinematic,
				Interpolation = rigidbody.interpolation,
				CollisionDetectionMode = rigidbody.collisionDetectionMode,
				Constraints = rigidbody.constraints
			};
		}
	}
}
