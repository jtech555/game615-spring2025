# Small Talk

## A STAGE: %x% starts chit chatting with %y%
kind: Normal chit chat
impress: Show off knowledge through small talk
flirt: Flirty chit chat
**NEXT:ALL**

-----------------------------
-----------------------------
-----------------------------

## B STAGE: %x% likes %y%
kind: Just a normal chat chat response to someone %x% likes
    NOWRULE: friend(x,y,>,5) + 1
    EFFECT: friend(x,y,+,1)
impress: %x% thinks they know what %y% knows and more!
    EFFECT: respect(x,y,+,1)
flirt: Flirty chit chat right back at %y%!
    EFFECT: attraction(x,y,+,1)
**NEXT:ALL**

## B STAGE: %x% doesn't really like %y%
kind: Politely end the conversation
    EFFECT: respect(x,y,-,1)
rude: Ok, whatever
    EFFECT: friend(x,y,-,1)
    EFFECT: respect(x,y,-,1)
**NEXT:ALL**

## B STAGE: %x% REALLY doesn't like %y%
kind: Stop the conversation
    NOWRULE: friend(x,y,>,3) - 10
    EFFECT: friend(x,y,-,1)
rude: Shut them down!
    NOWRULE: friend(x,y,<,3) - 10
    EFFECT: friend(x,y,-,1)
    EFFECT: respect(x,y,-,2)
**NEXT:NONE**

-----------------------------
-----------------------------
-----------------------------

## A STAGE: Having a fine time
kind: 
flirt: 
**NEXT:NONE**

## A STAGE: Not having a good time
kind: 
rude: 
flirt: 
**NEXT:Try to salvage a bad conversation**

-----------------------------
-----------------------------
-----------------------------

## B STAGE: Try to salvage a bad conversation
kind: 
rude: 
flirt: 
impress: 
**NEXT:Response to the salvage**

-----------------------------
-----------------------------
-----------------------------

## A STAGE: Response to the salvage
kind: 
rude: 
flirt: 
impress: 
**NEXT:NONE**