using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReaction : MonoBehaviour
{
    public Sprite exclamationMark;  // !（驚き）
    public Sprite questionMark; // ?（はてな）

    private SpriteRenderer sprt;    // SpriteRenderer格納用

    public enum PLAYER_REANCTIONS   // ←　（☆）enum型のいいところ
                                    //          ・クラスとしても使用できる（public PLAYER_REACTIONS playerReactions()みたいな）
                                    //          ・キャストしてやることで他の型として
                                    //          ・switch文と相性良
    {
        NONE,   // ノーリアクション
        EXCLAMATION,    // ！（驚き）
        QUESTION    // ？（はてな）
    }

    void Start()
    {
        sprt = this.GetComponent<SpriteRenderer>();
    }

    public void Reaction(PLAYER_REANCTIONS reaction)
    {
        switch (reaction)
        {
            case PLAYER_REANCTIONS.NONE:
                // ねこノーリアクション
                this.sprt.sprite = null;
                break;

            case PLAYER_REANCTIONS.EXCLAMATION:
                // ねこびっくり
                this.sprt.sprite = exclamationMark;
                break;

            case PLAYER_REANCTIONS.QUESTION:
                // ねこはてな
                this.sprt.sprite = questionMark;
                break;
        }
    }
}
