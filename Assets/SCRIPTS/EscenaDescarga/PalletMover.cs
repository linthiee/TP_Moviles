using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletMover : ManejoPallets {

    private MoveType miInput;
    public enum MoveType {
        WASD,
        Arrows,
        Tactile
    }

    public enum ScreenSide { Left, Right }
    public ScreenSide mySide;
    
    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool processedSwipe = true;    
    
    private void Start() 
    {
        if (Application.isMobilePlatform) 
        {
            miInput = MoveType.Tactile;
        } 
        else 
        {
            if (mySide == ScreenSide.Left) 
            {
                miInput = MoveType.WASD;
            } 
            else 
            {
                miInput = MoveType.Arrows;
            }
        }
    }
    
    private void Update() {
        switch (miInput) {
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.A)) {
                    PrimerPaso();
                }
                if (Tenencia() && Input.GetKeyDown(KeyCode.S)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.D)) {
                    TercerPaso();
                }
                break;
                
            case MoveType.Arrows:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.LeftArrow)) {
                    PrimerPaso();
                }
                if (Tenencia() && Input.GetKeyDown(KeyCode.DownArrow)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.RightArrow)) {
                    TercerPaso();
                }
                break;
                
            case MoveType.Tactile:
                DetectSwipe();
                break;
        }
    }

    private void DetectSwipe()
    {
        float swipeThreshold = Screen.width * 0.05f;
        
        foreach (Touch touch in Input.touches)
        {
            bool validTouch = false;
            if (mySide == ScreenSide.Left && touch.position.x < Screen.width / 2.0f)
            {
                validTouch = true;
            } else if (mySide == ScreenSide.Right && touch.position.x > Screen.width / 2.0f)
            {
                validTouch = true;
            }

            if (validTouch)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                    processedSwipe = false;
                } 
                else if (touch.phase == TouchPhase.Moved && !processedSwipe) 
                {
                    endTouchPosition = touch.position;
                    
                    if (Vector2.Distance(startTouchPosition, endTouchPosition) > swipeThreshold) 
                    {
                        ProcessSwipeDirection();
                        processedSwipe = true; 
                    }
                }
            }
        }
    }
    
    private void ProcessSwipeDirection() 
    {
        float xDistance = endTouchPosition.x - startTouchPosition.x;
        float yDistance = endTouchPosition.y - startTouchPosition.y;

        if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance)) 
        {
            if (xDistance < 0)
            {
                if (!Tenencia() && Desde.Tenencia()) {
                    PrimerPaso();
                }
            } else
            {
                if (segundoCompleto && Tenencia()) 
                {
                    TercerPaso();
                }
            }
        } 
        else
        {
            if (yDistance < 0) 
            {
                if (Tenencia()) 
                {
                    SegundoPaso();
                }
            }
        }
    }
    
    public void PrimerPaso() {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    public void SegundoPaso() {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    public  void TercerPaso() {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor) {
        if (Tenencia()) {
            if (receptor.Recibir(Pallets[0])) {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool Recibir(Pallet pallet) {
        if (!Tenencia()) {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}
