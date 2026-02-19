using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace reign
{
    [RequireComponent(typeof(Animator), typeof(Graphic))]
    public abstract class Animated : MonoBehaviour, IOriginAwake
    {
        public Animator Animator_Animator {get; private set;}
        public Graphic Graphic_Graphic; 
        public string string_CurrentAnimation {get; private set;} = "";
        public virtual void PlayAnimation(string ANIMATIONNAME)
        {
            if(Animator_Animator == null) return;
            
            Animator_Animator.StartPlayback();
            Animator_Animator.Play(ANIMATIONNAME);
            string_CurrentAnimation = ANIMATIONNAME;
        }
        public virtual IEnumerator CrossfadeColor(Color TARGET, float DURATION)
        {
            Color Color_StartColor = Graphic_Graphic.color;
            float float_Elapsed = 0f;

            while (float_Elapsed < DURATION)
            {
                float_Elapsed += Time.float_DeltaTime;
                float t = float_Elapsed / DURATION;

                Graphic_Graphic.color = Color.Lerp(Color_StartColor, TARGET, t);

                yield return null;
            }

            Graphic_Graphic.color = TARGET;
        }

        public virtual IEnumerator CrossfadeAlpha(float ALPHA, float DURATION)
        {
            float float_StartAlpha = Graphic_Graphic.color.a;
            float float_Elapsed = 0f;

            while (float_Elapsed < DURATION)
            {
                float_Elapsed += Time.float_DeltaTime;
                float t = float_Elapsed / DURATION;

                Graphic_Graphic.color = new (Graphic_Graphic.color.r, Graphic_Graphic.color.g, Graphic_Graphic.color.b, Mathf.Lerp(float_StartAlpha, ALPHA, t));

                yield return null;
            }

            Graphic_Graphic.color = new(Graphic_Graphic.color.r, Graphic_Graphic.color.g, Graphic_Graphic.color.b, ALPHA);
        }
        public virtual void CrossfadeAnimation(string ANIMATIONNAME, float DURATION, bool OVERRIDE = false)
        {
            if(Animator_Animator == null) return;
            if (OVERRIDE || (string_CurrentAnimation != ANIMATIONNAME))
            {
                Animator_Animator.CrossFade(ANIMATIONNAME, DURATION);
                string_CurrentAnimation = ANIMATIONNAME;
            }
        }
        public virtual void OriginAwake()
        {
            Animator_Animator = GetComponent<Animator>();
            Graphic_Graphic = GetComponent<Graphic>();
        }
    }
}
