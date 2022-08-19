using UnityEngine;

namespace Hexed.Modules
{
    internal class AvatarFilter
    {
        public static void AdjustAvatar(GameObject gameObject)
        {
            gameObject.SetActive(false);

            AudioSource[] AudioSources = gameObject.GetComponentsInChildren<AudioSource>(true);
            HandleAudios(AudioSources);

            Light[] Lights = gameObject.GetComponentsInChildren<Light>(true);
            HandleLights(Lights);

            ParticleSystem[] Particles = gameObject.GetComponentsInChildren<ParticleSystem>(true);
            HandleParticles(Particles);

            Animator[] Animators = gameObject.GetComponentsInChildren<Animator>(true);
            HandleAnimators(Animators);

            Collider[] Colliders = gameObject.GetComponentsInChildren<Collider>(true);
            HandleColliders(Colliders);

            BoxCollider[] BoxColliders = gameObject.GetComponentsInChildren<BoxCollider>(true);
            HandleColliders(BoxColliders);

            CapsuleCollider[] CapsuleColliders = gameObject.GetComponentsInChildren<CapsuleCollider>(true);
            HandleColliders(CapsuleColliders);

            SphereCollider[] SphereColliders = gameObject.GetComponentsInChildren<SphereCollider>(true);
            HandleColliders(SphereColliders);

            Renderer[] Renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            HandleRenderers(Renderers);

            SkinnedMeshRenderer[] MeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            HandleMeshRenderers(MeshRenderers);

            Rigidbody[] Rigidbodys = gameObject.GetComponentsInChildren<Rigidbody>(true);
            HandleRigidbodys(Rigidbodys); ;

            gameObject.SetActive(true);
        }

        private static void HandleAudios(AudioSource[] Audios)
        {
            int Count = 30;

            if (Audios.Length > Count)
            {
                for (int i = Count; i < Audios.Length; i++)
                {
                    Object.DestroyImmediate(Audios[i], true);
                }
                Wrappers.Logger.Log($"Avatar with Overflow of {Audios} {Audios.Length}", Wrappers.Logger.LogsType.Protection);
            }
        }

        private static void HandleLights(Light[] Lights)
        {
            int Count = 20;

            if (Lights.Length > Count)
            {
                for (int i = Count; i < Lights.Length; i++)
                {
                    Object.DestroyImmediate(Lights[i], true);
                }
                Wrappers.Logger.Log($"Avatar with Overflow of {Lights} {Lights.Length}", Wrappers.Logger.LogsType.Protection);
            }
        }

        private static void HandleParticles(ParticleSystem[] Particles)
        {
            int Count = 90;

            if (Particles.Length > Count)
            {
                for (int i = Count; i < Particles.Length; i++)
                {
                    Object.DestroyImmediate(Particles[i], true);
                }
                Wrappers.Logger.Log($"Avatar with Overflow of {Particles} {Particles.Length}", Wrappers.Logger.LogsType.Protection);
            }
        }

        private static void HandleAnimators(Animator[] Animators)
        {
            int Count = 120;

            if (Animators.Length > Count)
            {
                for (int i = Count; i < Animators.Length; i++)
                {
                    Object.DestroyImmediate(Animators[i], true);
                }
                Wrappers.Logger.Log($"Avatar with Overflow of {Animators} {Animators.Length}", Wrappers.Logger.LogsType.Protection);
            }
        }

        private static void HandleColliders(Collider[] Colliders)
        {
            int Count = 50;

            if (Colliders.Length > Count)
            {
                for (int i = Count; i < Colliders.Length; i++)
                {
                    Object.DestroyImmediate(Colliders[i], true);
                }
                Wrappers.Logger.Log($"Avatar with Overflow of {Colliders} {Colliders.Length}", Wrappers.Logger.LogsType.Protection);
            }

            foreach (Collider collider in Colliders)
            {
                if (collider == null) continue;
                if (Wrappers.UnityWrappers.IsBadPosition(collider.transform.position) || Wrappers.UnityWrappers.IsBadRotation(collider.transform.rotation)) Object.DestroyImmediate(collider, true);
            }
        }

        private static void HandleRenderers(Renderer[] Renderers)
        {
            int Count = 350;

            if (Renderers.Length > Count)
            {
                for (int i = Count; i < Renderers.Length; i++)
                {
                    Object.DestroyImmediate(Renderers[i], true);
                }
                Wrappers.Logger.Log($"Avatar with Overflow of {Renderers} {Renderers.Length}", Wrappers.Logger.LogsType.Protection);
            }
        }

        private static void HandleMeshRenderers(SkinnedMeshRenderer[] Renderers)
        {
            int Count = 45;

            if (Renderers.Length > Count)
            {
                for (int i = Count; i < Renderers.Length; i++)
                {
                    Object.DestroyImmediate(Renderers[i], true);
                }
                Wrappers.Logger.Log($"Avatar with Overflow of {Renderers} {Renderers.Length}", Wrappers.Logger.LogsType.Protection);
            }

            foreach (SkinnedMeshRenderer renderer in Renderers)
            {
                if (renderer == null) continue;
                renderer.updateWhenOffscreen = false;
                renderer.sortingOrder = 0;
                renderer.sortingLayerID = 0;
                renderer.rendererPriority = 0;
            }
        }

        private static void HandleRigidbodys(Rigidbody[] Rigidbodys)
        {
            int Count = 30;

            if (Rigidbodys.Length > Count)
            {
                for (int i = Count; i < Rigidbodys.Length; i++)
                {
                    Object.DestroyImmediate(Rigidbodys[i], true);
                }
                Wrappers.Logger.Log($"Avatar with Overflow of {Rigidbodys} {Rigidbodys.Length}", Wrappers.Logger.LogsType.Protection);
            }
        }
    }
}
