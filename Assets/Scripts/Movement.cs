using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour

{
    public float speed  = 8.0f;
    public float speedMultiplayer = 1.0f;

    public Vector2 initialDirection;
    public LayerMask obstacleLayer;
    public new Rigidbody2D rigidbody2D {get; private set;}

    public Vector2 direction{get; private set;}
    public Vector2 nextDirection{get; private set;}
    public Vector3 startingPosition {get; private set;}

    private void Awake()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }
    
    private void Start() {
        ResetState();
    }

    public void ResetState (){
        this.speedMultiplayer = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody2D.isKinematic = false;
        this.enabled = true;

    }

    private void Update (){
        if (this.nextDirection!= Vector2.zero) {
            SetDirection(this.nextDirection);
        }
    
    }

    private void FixedUpdate (){
        Vector2 position = this.rigidbody2D.position;
        Vector2 translation = this.direction*this.speed*this.speedMultiplayer * Time.fixedDeltaTime;
        this.rigidbody2D.MovePosition(position + translation );
    }

    public void SetDirection(Vector2 direction , bool forced = false){

        if(forced || !Occupied(direction)){
            this.direction = direction;
            this.nextDirection =Vector2.zero;
        }
        else{
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction){
        RaycastHit2D hit2D = Physics2D.BoxCast(this.transform.position, Vector2.one*0.75f,0.0f, direction,1.5f, this.obstacleLayer);
        return hit2D.collider!= null;

    }
}
