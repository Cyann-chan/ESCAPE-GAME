# Histoire
Capturez et enfermez dans la maison de la sorcière, seule une potion magique vous permettra de sortir sain et sauf. A vous de la confectionner! Mais faites vite avant qu'elle n'arrive...

# Commandes
- Utilisez les portails verts et la gachette pour vous téléporter à travers la maison de la sorcière et chercher les indices pour vous échapper
- Avec la même gachette, attrappez les différents objets qui pourraient vous aider
- Le jeu se joue seulement avec la manette droite

# Indications
- Commencer la partie avec la scene "Menu"
- Dans la scene "WitchHut", il y a une modification à faire dans l'objet "HandInteractionManager" (qui gère tout le jeux, pas juste les mains...), un script y est attaché: il faut modifier le champ "Time Left" avec la valeur 360 :) Sinon vous aurez 5 secondes avant le Game Over...
