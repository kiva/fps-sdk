---
sidebar_position: 7
---

# Utilisation de l'API pour publier des projets de prêts
Nous recommandons à votre équipe technique de consulter le coordinateur Kiva de votre organisation afin de bien comprendre le processus de publication des projets de prêts Kiva. Pour ce faire, demandez à votre équipe technique d'examiner les points suivants :
* [Cette vidéo](https://www.youtube.com/watch?v=ubftp56tQXE&list=PLVJzkwcBFwLAb7zYjAdTfkuWRk0KP4Xnz&index=3) explique en détail comment publier un prêt individuel.
* [Cette vidéo](https://www.youtube.com/watch?v=eTyfJfTX0aA) explique en détail comment publier un prêt de groupe.
* [Consultez la section Nouveaux](https://kivapartnerhelpcenter.zendesk.com/hc/fr/categories/360001759932-Nouveaux-Credits) prêts pour plus d'informations, en particulier [l'étape 1 : Description - Prêts individuels](https://kivapartnerhelpcenter.zendesk.com/hc/fr/articles/360030919632-%C3%89tape-1-Description-Cr%C3%A9dits-individuels)  et l'étape [1 : Description - Prêts de groupe](https://kivapartnerhelpcenter.zendesk.com/hc/fr/articles/360031260191-%C3%89tape-1-Description-Cr%C3%A9dits-de-Groupe).
 
## Processus
1. Les nouvelles informations sur les prêts sont envoyées du SIG à PA2 via l'API.
2. Un projet de prêt est automatiquement créé. Les projets peuvent être consultés en cliquant sur le lien " Brouillons " dans la section " Prêts " de la page d'accueil de PA2.
3. Utilisation du point de terminaison de récupération des prêts de l'API.

## Informations complémentaires
* Avant que le prêt en question ne soit publié, PA2 effectuera tous les mêmes contrôles de validation que pour les prêts publiés sans l'API. Tous les messages d'erreur doivent être résolus avant que le prêt puisse être publié.
* Il n'y a pas de problème si l'API n'envoie pas d'informations pour certains champs nécessaires à la publication d'un prêt. Par exemple, tous les SIG ne stockent pas les champs spécifiques de Kiva tels que la description de l'emprunteur ou le secteur. Bien que ces champs soient nécessaires pour publier un prêt dans PA2, ils ne doivent pas être envoyés via l'API. Les informations relatives à ces champs peuvent être ajoutées manuellement après que le projet de prêt a été créé dans PA2. Notez que plus vous pouvez envoyer d'informations via l'API, plus le coordinateur Kiva gagnera du temps.
* Lorsque vous publiez un prêt collectif ou individuel, certains champs ne sont pas obligatoires. Si le prêt que vous publiez est un prêt individuel, et que vous soumettez des données spécifiques à un prêt collectif, PA2 vous donnera un message d'erreur et vous ne pourrez pas publier le prêt.
* Si un prêt individuel est comptabilisé, aucune des zones suivantes ne doit être incluse. Les champs suivants sont uniquement requis lors de la publication d'un prêt de groupe :
  * Group_name: il s'agit du nom du groupe qui apparaîtra sur Kiva.org.
  * Internal_client_id: est l'ID de chaque client représenté dans le groupe (par exemple, si un groupe compte trois membres, chacun d'entre eux peut avoir son propre ID de client à énumérer ici).
  * Internal_loan_id: il s'agit de l'ID de chaque prêt individuel (par exemple, un membre du groupe pourrait obtenir son troisième prêt auprès de l'organisation, et ce prêt pourrait avoir un ID unique). Entrez ce numéro d'identification ici)
  * Not_pictured: utilisez ce champ dans le cas où un emprunteur est listé à la rubrique 1 : Description n'apparaît pas dans la photo
* La photo de l'emprunteur peut être envoyée à PA2 avec l’API.
* Les tableaux de remboursement peuvent être envoyés dans plusieurs formats différents. Si vous pensez qu'un format différent de l'exemple ci-dessous conviendrait mieux à votre organisation, veuillez en informer Kiva et nous vous fournirons de plus amples informations.
* Pour vérifier si le document JSON que vous avez créé est correct, vous pouvez utiliser un validateur JSON en ligne comme celui-ci : https://jsonlint.com/ .

## Documentation technique
Toda la documentación técnica de Kiva, incluidos los puntos finales, puede encontrarse aquí:
 * Environnement de test (Stage) https://partner-api-stage.kiva.org/swagger-ui/
 * Production (à utiliser après les tests)  https://partner-api.kiva.org/swagger-ui/

