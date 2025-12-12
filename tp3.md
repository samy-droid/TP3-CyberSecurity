# Rapport TP3 de Cybersec NOM PRENOM

## Attaque 1: BD fuitée et mot de passe

1. Etape 1 
Trouver l'emplacement de la BD en utilisant System Informer
![Titre de l'image](im1.png)

2. Etape 2 
Ouvrir la BD avec dataGrip afin de trouver les mot de passes hachées.
![Titre de l'image](im2.png)

3. Trouver les mots de passes des premiers ministres avec crackStation ( sauf pour Stephen Harper et Paul Martin).

Justin trudeau : Passw0rd1!
Jean Chrétien : shawinigan
Kim Campbell : 	GirlPower
Mark carney : 	fortsmith

![Titre de l'image](im3.png)

![Titre de l'image](im4.png)



### Correctif implanté

Description du correctif.

Pour plus de sécurité on va modifier le code source de l'application
Pour ce faire on va changer le hachage md5  avec un hachage avec BCrypt qui va utiliser un sel unique et un hachage plus sécurisé.

1. Étape 1
Télécharger la librairie BCrypt :

![Titre de l'image](im5.png)

2. Étape 2
Remplacer le hachage MD5 en BCrypt

![Titre de l'image](im6.png)

Preuve que l'attaque ne fonctionne plus avec étapes + copie d'écran

1. On peut clairement voir que le hachage est dans un autre format plus difficile à cracker

![Titre de l'image](im7.png)

2. Malgré que j'ai mis 1234 comme mot de passe crackstation n'arrive pas à le déchiffrer donc le correctif fonctionne parfaitement.
![Titre de l'image](im8.png)


## Attaque 2: BD fuitée et encryption

1. Etape 1 
On a déjà accès à la base de données donc on va essayer de trouver la logique d'encryption en faisant des tests de NAS de 1 à 9.

![Titre de l'image](im9.png)

Après ces tests on peut voir que :
1=b
2=d
3=f
4=h
5=j
6=l
7=n
8=p
9=r

2. Etape 2 
Juste pour tout confirmer j'ai créée un NAS : 123456789 et il m'a donné comme résultat : bdfhjlnpr comme prévu.

![Titre de l'image](im10.png)


### Correctif implanté

Court descriptif du correctif 

1. On va utiliser un algo d'encryption classique pour les NAS, pour y arriver on va utiliser BlowFish
On commence avec l'installation du paquet Portable.BouncyCastle

![Titre de l'image](im12.png)

2. On implante du code C# pour faire de la crypto symétrique et crypter les NAS avec BlowFish


![Titre de l'image](im11.png)

Preuve que l'attaque ne fonctionne plus avec étapes + copie d'écran

3. Maintenant le format de cryptage des NAS a changé et on ne peut plus suivre une logique pour deviner la logique d'encryption.


![Titre de l'image](im13.png)


## Attaque 3 Injection SQL

1. On va injecter du code SQL destructrice pour écraser la table MUtilisateur.
On commence par se connecter puis dans le champ nom on ajoute le code SQL comme dans la capture d'écran. Ensuite on met n'importe quel mot de passe. La connexion va bien sur échouer, la première requête va pas fonctionner non plus mais la deuxième requête pour supprimer la table MUtilisateur va fonctionner. 

![Titre de l'image](im14.png)

2. Preuves que la table MUtilisateur a bien été supprimé

![Titre de l'image](im15.png)
3. 

### Correctif implanté

Description du correctif.

Dans le code il y a 3 endroits où il y a des failles car il y a trop de concaténation de chaines de  caractère (strings). Ce qui le rend vulnérable car on peut insérer du code sql malveillant et faire des dégats comme montré précédemment avec le code DROP TABLE....

![Titre de l'image](im16.png)

![Titre de l'image](im17.png)


Pour le corriger on a modifié le code en utilisant des paramètres SQL. Donc toutes les requêtes ont été réecrites avec des paramètres SQL. Ce qui fait que les données des utilisateurs sont traités comme des valeurs avec le @.

![Titre de l'image](im18.png)

![Titre de l'image](im19.png)

![Titre de l'image](im20.png)

Preuve que l'attaque ne fonctionne plus avec étapes + copie d'écran
