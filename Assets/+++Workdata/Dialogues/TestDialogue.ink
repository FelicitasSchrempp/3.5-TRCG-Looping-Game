=== Hoardinger === 

= FirstHello
Hoardinger: Eh?! Who are you?! What do you want?! I know I don't anything from you. ... Except perhaps those shiny shards of yours, hmm. Fine, fine, fine, we can trade if you insist.

Hoardinger: Well then, welcome to Hoardinger's Hoards, only looking, no touchy-touchy!
->END

= HelloLowPrizes
Hoardinger: Welcome to Hoardinger's Hoards, only looking, no touchy-touchy!
->END

= HelloHighPrizes
Hoardinger: Welcome to Hoardinger's Hoards, only looking, no touchy-touchy!

Hoardinger: Oh, I'm so sorry, the prizes have gone up today due to... inflation, yes, yes, and the shippping via donkey express has enourmous prizes nowadays, you have no idea, I'm actually doing you favour here, risking my own finacial stance for the likes of you, and then there are all those dreadful tariffs and don't get me started on the waylaying pixies and scoundrels, always targetting poor, poor Hoardinger and -- well, the prizes have gone up, deal with it.
->END

= BuyHealth
buy health for PRIZE?
+[Yes]
~ Event("BuyHealth")
~ Event("BackToShop")
->Else
+[No]
~ Event("BackToShop")
->Else

= BuyMana
buy mana for PRIZE?
+[Yes]
~ Event("BuyMana")
~ Event("BackToShop")
->Else
+[No]
~ Event("BackToShop")
->Else

= BuyHat
buy hat for PRIZE?
+[Yes]
~ Event("BuyHat")
~ Event("BackToShop")
->Else
+[No]
~ Event("BackToShop")
-> Else

= BuySpell
buy spell for PRIZE?
+[Yes]
~ Event("BuySpell")
~ Event("BackToShop")
->Else
+[No]
~ Event("BackToShop")
->Else

= Else
Hoardinger: Anything else you want?
->END

= Bye
Hoardinger: Hope your not coming back too soon!
->END 

= Empty
Hoardinger: Alright, that's it. You've stolen enough of my treasures for today. Go away.
->END

= FirstMeeting
Eh?! Who are you?! What do you want?! I know I don't anything from you. ... Except perhaps those shiny shards of yours, hmm. Fine, fine, fine, we can trade if you insist.
->END

... haaahhh, my precious, little thingies, you're all mine...
Heh?! Grrr, what do you want?! You'll never get my-- Ooooh, shiny! Gimme, gimme!
Player gets option for giving currency
introduction continues

= NormalApproach
Who'S there?! Oh. It's you. Again. Have you come to rob me of my beloved treasures again, have you?
->END

... one sparkling gemmy, two sparkling gemmies, FOUR sparkling gemmies, harharharh... Ieehh! It's you. Where are my shinies?!

= Shopname
Welcome to Hoardinger's Hoards and Herbs. Here, you'll find anything your heart desires, except herbs, those just sounded nice in the name.
->END

Welcome to Hoardinger's Hoards Emporium, the greatest collection of treasures in all the kingdom!

Welcome to Hoardinger's Hoards, only looking, no touchy-touchy!

Welcome to Hoardinger's Loads and Hoards, here, you'll find the finest of treasures. Whether or not you'll get to take with you, is another story though.

= ExtraCharge
Oh, I'm so sorry, the prizes have gone up today due to... inflation, yes, yes, and the shippping via donkey express has enourmous prizes nowadays, you have no idea, I'm actually doing you favour here, risking my own finacial stance for the likes of you, and then there are all those dreadful tariffs and don't get me started on the waylaying pixies and scoundrels, always targetting poor, poor Hoardinger and -- well, the prizes have gone up, deal with it.
->END

NOoOo!!! You can't take my thingy like this, nonono, you mustn't take it, no... What? You have more shinies for me? ... hmmmm... I don't know... Alright, shiny is shiny, gimme, gimme!

= Byeold
Alright, there you go. Thanks for the shards and so on. Now, please leave.
->END

Thank you. I hope you never come back.

Always a pleasure making business with you. Toodle-oo!

Alright, that's it. You've stolen enough of my treasures for today. Go away.

No? No more shinies? Then I won't give you anything anymore!

=== CrystalGuard ===

= CantGetIn1
Cart the Shard: I was told to not let anyone get into the desert beyond. So I won't, not even you.
-> END

= CantGetIn2
Cart the Shard: I stand by my orders. You're not passing.
-> END

= Vanish
Cart the Shard: ... Oh well, screw my orders. Do whatever you like.
~Event("Vanish")
-> END

= PopUp
Cart the Shard: Hello there. Just so you know, you've left your oven on.
~Event("VanishAgain")
-> END

=== PuristGuard ===

= CantGetIn1
Bart the Guard: I'm afraid I can't let you pass.
->END

= CantGetIn2
Bart the Guard: YOU SHALL NOT PASS!
->END

=== CultistGuard ===

= CantGetIn1
Lart the Mart: Sorry mate, no getting through here today.
->END

= CantGetIn2
Lart the Mart: I'm serious, mate. You don't want to meet the guys beyond this gate, they're insane.
->END

=== TechnoGuard ===

= CantGetIn1
Gart the Barb: Nope, here's nothing to see at all.
->END

= CantGetIn2
Gart the Barb: No groove, no move.
->END

=== UI ===

= GetPotion
You found a Health potion - Hooray!
->END

= GetManaPotion
You found a Mana potion - Hooray!
->END

= GetCrystalWall
You found the Crystal Wall Spell - Check it out in the Spell Menu!
->END

= GetCrystalAoE
You found the Crystal Pillar Spell - Check it out in the Spell Menu!
->END

= GetCrystalHealing
You found the Crystal Cocoon Spell - Check it out in the Spell Menu!
->END

= GetCrystalDart
You found the Crystal Dart Spell - Check it out in the Spell Menu!
->END

= GetTeleport
You found the Teleport Ability!
->END

= ManaMax
Your Mana has been maxed out!
->END

=== Council ===

= Talk1
Council: Oh hey, it's you. Haven't seen you in a while.

Council: What? The crystal shattered?! And you think the root of this evil lies in the desert south-east from here? Well, you saw it, you fix it. Here's a permit to leave town, just show it to the guard and you'll be able to pass.

~Event("GuardGone")

Council: Now, off you go, we've got cards to play.
->END

= Talk2
Council: Wait, you are saying that one of our council brothers brought this upon us? How will we ever find the imposter, it's impossible!
->END

= Talk3
Council: You're still here? Don't you need to save the world or something?
->END

