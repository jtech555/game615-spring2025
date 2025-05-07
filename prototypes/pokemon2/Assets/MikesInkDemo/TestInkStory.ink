// This is a basic example
== IntroductoryScene ===
"[Awaiting Space Bar Press]"
    -> DONE
-> DONE
//-> pause0


VAR person1 = "N/A"
VAR player = "N/A"
VAR status="N/A"
VAR charisma="N/A"
VAR talk1Over="false"
VAR talk2Over="N/A"
VAR pokeballs="N/A"
VAR choice=0
VAR bridgeUnlocked="false"
VAR shakeStart="false"
VAR mewConvoEnded="false"

//anderson - 2hours

== hatManKnot ===
He took them all! All my Pokemon!
    *Who took them?!?
        He calls himself Mewtwo.. A new Pokemon I've never seen before..
                **A Pokemon stealing peoples Pokemon? How can this be??
                    I don't know. He mentioned something about being created in Proffesor Treanors lab, and that all Pokemon should fight to the death. Please Ash you have to get them back! Look what he did to my Blastoise!
                            ***Turned to stone.. but how can this be...
                                I don't know.. I couldn't beat him Ash. He took them all! Please.. you have to do something!
                                    ~talk1Over="true"
                                    ->DONE
                **I know that Pokemon...
                    He mentioned something about being created in Proffesor Treanors lab, and that all Pokemon should fight to the death. Please Ash you have to get them back! Look what he did to my Blastoise!
                            ***Turned to stone.. but how can this be...
                                I don't know.. I couldn't beat him Ash. He took them all! Please.. you have to do something!
                                    ~talk1Over="true"
                                    ->DONE
    *Was that who was running?!
        He calls himself Mewtwo.. A new Pokemon I've never seen before..
                **A Pokemon stealing peoples Pokemon? How can this be??
                    I don't know. He mentioned something about being created in Proffesor Treanors lab, and that all Pokemon should fight to the death. Please Ash you have to get them back! Look what he did to my Blastoise!
                            ***Turned to stone.. but how can this be...
                                I don't know.. I couldn't beat him Ash. He took them all! Please.. you have to do something!
                                    ~talk1Over="true"
                                    ->DONE
                **I know that Pokemon...
                    He mentioned something about being created in Proffesor Treanors lab, and that all Pokemon should fight to the death. Please Ash you have to get them back! Look what he did to my Blastoise!
                            ***Turned to stone.. but how can this be...
                                I don't know.. I couldn't beat him Ash. He took them all! Please.. you have to do something!
                                    ~talk1Over="true"
                                    ->DONE

== Professor ==
I never should of created him!! How could I be so stupid!??
    *Create who??
        Mewtwo!.. I thought by creating a new breed of Pokemon, I could change things.. now he's stolen all the pokemon we had here!
            **Theres no time for that professor we have to leave
                He means to make them battle each other Ash! A fight to the death! Making each kind battle's it's own.. turning anyone who opposes or loses into stone...
                        ***We can't let that happen!
                                Theres only one way to stop him. We need to find a pokemon of his breed, Mew..
                                        ~talk2Over="true"
                                        -> Mewtwo
                                        
                        ***We need to go Proffesor!
                                Theres only one way to stop him. We need to find a pokemon of his breed, Mew.. 
                                        ~talk2Over="true"
                                        -> Mewtwo
            **Stolen?? What's his plan??
                He means to make them battle each other Ash! A fight to the death! Making each kind battle's it's own.. turning anyone who opposes or loses into stone...
                        ***We can't let that happen!
                                Theres only one way to stop him. We need to find a pokemon of his breed, Mew.. 
                                        ~talk2Over="true"
                                        -> Mewtwo
                        ***We need to go Proffesor!
                                Theres only one way to stop him. We need to find a pokemon of his breed, Mew.. 
                                        ~talk2Over="true"
                                        -> Mewtwo

== Mewtwo ==
Mewtwo: You fools.. Mew has been trapped in the Mountain behind you for YEARS. He can never be freed. I forbid it! And you know what.. Hand over that pikachu! He will need to fight as well!
        *Pikachu is mine!! Never!!
            Mewtwo: I didn't ask.. Good luck unlocking Mew.. Punny human.. With my mind.. I command the land to CRUMBLE! Mwahahaha! Goodbye. For now..
                ~shakeStart="true"
    ->DONE
    
== Mew ==
After many years I am free.. thank you.. Thanks to Mewtwo's psychic attack, it uncovered the lever and my safe after all these years.. 
    *How do we defeat Mewtwo??
        Him and I must battle..... I, like him, am extremly powerfull..
            **Say what??
                We are both human made pokemon, who don't need a Trainer. When humans noticed this, they locked me away from this world as I slept here..
                        *** Can you beat Mewtwo?
                            If I miss just one time, the ground and world will turn to stone.. But if I hit him just once.. he will become stone, and breath no more..
                            **** You cant fight then! The worlds at stake!
                                It is the only way Ash.. I sense now that Mewtwo has your pokemon at the colosseum... having him now fight another.. I must now join the fight now too..
                                        *****The colosseum??
                                            It is right next to this mountain.. But go Ash! Leave this place.. 
                                            ******This world is my home and Pikachu's my best friend! I can't let this happen!
                                                Don't get in the way Ash.. I beg of you.
                                            ~mewConvoEnded="false"
                                            ->DONE
                                    
                                
                            
                 
           
            
== ManByTheBridge ==
{talk1Over=="true" && bridgeUnlocked=="false": 
Ok we can talk Ash.
        * Jimmy, the PokeCenter just blew up!
            Oh shoot! I know you probably need to get there but please come back when you have 5 pokeballs. Then I can let you cross.
                    **[Roll to Intimidate]
                        ~choice = RANDOM(1, 5)
                      
                        {choice==1: 
                        You rolled a {choice}. You tell Jimmy you have the bestest coolest, neatest badges in all the land. Jimmy now thinks your dilusional.
                            ->DONE
                        }
                        {choice==2: 
                        You rolled a {choice}. You stare at Jimmy menacingly... raising your finger to point at him, to tell him to move, but you end up poking your eye. Jimmy almost laughs to death.
                            ->DONE
                        }
                        {choice==3: 
                        You rolled a {choice}. You declare a thumb war against Jimmy to cross. it results in a tie and nothing happens.
                            ->DONE
                        }
                        {choice==4: 
                        You rolled a {choice}. You tell Jimmy you're the only one who can save this place and to let him through! Jimmy proceeds to look at the fire in the distance. Jimmy allows you to cross.
                            ~bridgeUnlocked="true"
                            ->DONE
                        }
                        {choice==5: 
                        You rolled a {choice}. You tell Jimmy to move now. Jimmy then soon remembers who on earth you are.. Ash Ketchum - the 10 year old boy who defeated EVERY pokemon gym leader EVER. Jimmy allows you to cross.
                             ~bridgeUnlocked="true"
                             ->DONE
                        }
                    **Ugh. Fine.
                        ->DONE
        * I have 5 pokeballs!
        {pokeballs>=5: 
        Well done! You may cross!
        ~pokeballs=pokeballs-5
        ~bridgeUnlocked="true"
        ->DONE
        }
        {pokeballs<5: 
        Lies. Come back when you have 5
        ->DONE
        }
                        

}
{talk1Over=="false": 
Ash! The Pokemon Master! Good to see you again!
    * You too ! Say, did you just see something move across the canyon?
        I did! I think it was a Pokemon. It just teleported through it like it was nothing!.. You should talk to that girl over there before we talk more though... she looks upset. 
                    ->DONE

}



== hatManKnotDialog2 ==
* Yes. What is this place?
    I'm glad you asked! This is the Winterhold Castle! And I am the Lead Recruitment Officer. Would you like to join the Knights Watch?? People say it makes them much more attractive..
    **Yes! Count me in!
        You got it! You are now Rank 1, a Knight's Watch Cleaner (Rank 1). You can rank up by performing more tasks in the area. Carry on now and glad to have you!
        ~status++
        ~charisma++
        -> DONE
    **No.. I'm not attractive..
        What?? So honest.. Honesty is an attractive quality you know. I'm not taking no for an answer. We need honest and charismatic men like you! Welcome, newly appointed Knight's Watch Cleaner!
        ~status++
        ~charisma++
        -> DONE
* No. I know exactly where I am!
    That's right! Your'e at the one and only Winterhold Castle! And I am the Lead Recruitment Officer. Would you like to join the Knights Watch?? People say it makes them much more attractive..
    **Yes! Count me in!
        You got it! You are now the Knight's Watch Cleaner. You can rank up by performing more tasks in the area. Carry on now and glad to have you!
        ~status++
        ~charisma++
        -> DONE
    **No.. I'm not attractive..
        What?? So honest.. Honesty is an attractive quality you know. I'm not taking no for an answer. We need honest and charismatic men like you! Enjoy your starting rank as a Knight's Watch Cleaner!
        ~status++
        ~charisma++
        -> DONE



== stormTrooperKnot ===
{status == 4:
    All hail me! I mean.. the new Lord Commander!
    -> DONE
}
{status == 3: 
    The secret to increasing your rank, is knowing me, Giddeon. I am secretly the Queens son. No one here knows!! Would you like me to increase your rank?
            *Yes!
                Done. You are now Lord Commander of the Knight's Watch!
                ~status++
                -> DONE
            *Why are you a Guard though?
                After growing up, I hated all the attention being a Prince, especially not being able to leave my room.. Being a Guard no one can tell it is me under this helmet!
                        **You are Genius!
                            And you just made my day! :) Enjoy your new rank.. Lord Commander!
                                ~status++
                                -> DONE
                        **Giddeon you're wearing 3 gold rings
                            And this is why I'm increasing your rank! Thanks for telling me, our new Lord Commander!
                                ~status++
                                -> DONE
}
{status == 1: 
    Hello Fellow Member of the Knight's Watch! I am Giddeon. One of the Knights Watch Guards. Speak to me when you're at Rank 3 (a Knight!) and I will tell you a secret to increase your rank!
}
{status == 0: I only speak to Members of the Knights Watch.}
    -> DONE
-> DONE


== femaleLegoKnot ===
{charisma >= 3: 
    So charismatic you are!! I can just tell! =] 
}
{charisma == 2: 
    Hi Indy!! =] 
}
{charisma == 1: 
    Ugh. I guess I can talk to you. 
    * Who are you??
        I'm the Queen's sister. I'm just feeling so miserable today! 
            ** I will fix it.
                Speaking like that.. you just did :)
                    ~charisma++
                    -> DONE
            ** I must go.
                The way you looked at me when you said that though. There is something too you..
                ~charisma++
                -> DONE
    * Are you ok?
        How can I be ok when I am the Queen's sister? Life is always so miserable!!
            ** I will fix it.
                Speaking like that.. you just did :)
                    ~charisma++
                    -> DONE
            ** I must go.
                The way you looked at me when you said that though. There is something too you..
                ~charisma++
                -> DONE
   
}
{charisma == 0: Ew get away from me!!}
    -> DONE
-> DONE


== femaleLegoKnot2 ===
{charisma >= 3: 
    There is a sense of Charisma to you! Maybe it's your aura!
    -> DONE
}
{charisma == 2: 
    Oh my. Who are you??
        ** My name is Indy. And I am the Hero this place needs!
            Yes you are! Speaking like that I am making you a Guard! I'm Danny. The Queen of this place!
                ~charisma++
                ~status++
                    *** I will vanquish any foe! I will defeat any dragon! I will defend the Knight's Watch!
                            Say no more!!
                                ~charisma+=1
                                    ****No more.
                                            There is such a charm to you. Promoted you shall be!
                                            ~charisma+=1
                                            ~status++
                                            *****I live to protect the Knight's Watch.
                                            Yes! Carry on!!
                                                -> DONE
                                    **** No.
                                        I like someone who doesn't bow down to me. A higher rank you shall be!
                                            ~charisma+=1
                                            ~status++
                                            *****I live to protect the Knight's Watch.
                                            Yes! Carry on!!
                                                -> DONE
                    *** I must go, my Queen...
                            Say no more!!
                                ~charisma+=1
                                    ****No more.
                                            There is such a charm to you. Promoted you shall be!
                                            ~charisma+=1
                                            ~status++
                                            *****I live to protect the Knight's Watch.
                                            Yes! Carry on!!
                                                -> DONE
                                    **** No.
                                        I like someone who doesn't bow down to me. A higher rank you shall be!
                                            ~charisma+=1
                                            ~status++
                                            *****I live to protect the Knight's Watch.
                                            Yes! Carry on!!
                                                -> DONE
        ** Please, call me Doctor Jones..
            Oh a Doctor?? Oh my.. I'm Danny. The Queen of this place!
                ~charisma++
                ~status++
                    *** I will vanquish any foe! I will defeat any dragon! I will defend the Knight's Watch!
                            Say no more!!
                                ~charisma+=1
                                    ****No more.
                                            There is such a charm to you. Promoted you shall be!
                                            ~charisma+=1
                                            ~status++
                                            *****I live to protect the Knight's Watch.
                                            Yes! Carry on!!
                                                -> DONE
                                    **** No.
                                        I like someone who doesn't bow down to me. A higher rank you shall be!
                                            ~charisma+=1
                                            ~status++
                                            *****I live to protect the Knight's Watch.
                                            Yes! Carry on!!
                                                -> DONE
                    *** I must go, my Queen...
                            Say no more!!
                                ~charisma+=1
                                    ****No more.
                                            There is such a charm to you. Promoted you shall be!
                                            ~charisma+=1
                                            ~status++
                                            *****I live to protect the Knight's Watch.
                                            Yes! Carry on!!
                                                -> DONE
                                    **** No.
                                        I like someone who doesn't bow down to me. A higher rank you shall be!
                                            ~charisma+=1
                                            ~status++
                                            *****I live to protect the Knight's Watch.
                                            Yes! Carry on!!
                                                -> DONE
}
{charisma == 1: 
I'm Danny, the Queenn. Hi...
}
    -> DONE
{charisma == 0: Ew!! Get away from me!!}
    -> DONE
-> DONE

// This knot demonstrates how you can create
// parameterized / generalized patterns of 
// conditional conversation between characters
// This relies on the following variables being set before being called

//VAR player_charisma = -1
//..VAR responder_affinityTowardPlayer = -1


//=== TalkToCharacter ===
//Hello, {responder}.
//* Did you know...
//    Did you know that you can see the moon in sky during the day sometimes!?!
//    -> DONE
//* {player_charisma > 5}[Make suave comment...]
//    Careful, keep looking at me like that and I’ll start thinking you mean it.
//    {responder_affinityTowardPlayer > 4: 
//        {responder} keeps looking at {player} like that.
//        -else: 
//            {responder} rolls their eyes.
  //  }
//    -> DONE




