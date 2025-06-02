---
sidebar_position: 4
---

# Utilisation de l'API pour publier des projets de prêts
Nous recommandons à votre équipe technique de consulter le coordinateur Kiva de votre organisation afin de bien comprendre le processus de publication des projets de prêts Kiva. Pour ce faire, demandez à votre équipe technique d'examiner les points suivants :
* [Cette vidéo](https://www.youtube.com/watch?v=9gScexv-yZo&amp;t=5s) explique en détail comment publier un prêt individuel.
* [Cette vidéo](https://www.youtube.com/watch?v=KvKUScWF73M&amp;t=1s) explique en détail comment publier un prêt de groupe.
* Consultez la section Nouveaux prêts pour plus d'informations, en particulier [l'étape 1 : Description - Prêts individuels](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360030919632) et [l'étape 1 : Description - Prêts de groupe](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360031260191).
 
## Processus
* Les nouvelles informations sur les prêts sont envoyées du SIG à PA2 via l'API.
* Un projet de prêt est automatiquement créé. Les projets peuvent être consultés en cliquant sur le lien " Brouillons " dans la section " Prêts " de la page d'accueil de PA2.
* Utilisation du point de terminaison de récupération des prêts de l'API.

## Informations complémentaires
Avant que le prêt en question ne soit publié, PA2 effectuera tous les mêmes contrôles de validation que pour les prêts publiés sans l'API. Tous les messages d'erreur doivent être résolus avant que le prêt puisse être publié.

Il n'y a pas de problème si l'API n'envoie pas d'informations pour certains champs nécessaires à la publication d'un prêt. Par exemple, tous les SIG ne stockent pas les champs spécifiques de Kiva tels que la description de l'emprunteur ou le secteur. Bien que ces champs soient nécessaires pour publier un prêt dans PA2, ils ne doivent pas être envoyés via l'API. Les informations relatives à ces champs peuvent être ajoutées manuellement après que le projet de prêt a été créé dans PA2. Notez que plus vous pouvez envoyer d'informations via l'API, plus le coordinateur Kiva gagnera du temps.

Lorsque vous publiez un prêt collectif ou individuel, certains champs ne sont pas obligatoires. Si le prêt que vous publiez est un prêt individuel, et que vous soumettez des données spécifiques à un prêt collectif, PA2 vous donnera un message d'erreur et vous ne pourrez pas publier le prêt.

Si un prêt individuel est comptabilisé, aucune des zones suivantes ne doit être incluse. **Les champs suivants sont uniquement requis lors de la publication d'un prêt de groupe :**
* `group_name`: il s'agit du nom du groupe qui apparaîtra sur Kiva.org.
* `internal_client_id`: est l'ID de chaque client représenté dans le groupe (par exemple, si un groupe compte trois membres, chacun d'entre eux peut avoir son propre ID de client à énumérer ici).
* `internal_loan_id`: il s'agit de l'ID de chaque prêt individuel (par exemple, un membre du groupe pourrait obtenir son troisième prêt auprès de l'organisation, et ce prêt pourrait avoir un ID unique). Entrez ce numéro d'identification ici)
* `not_pictured`: utilisez ce champ dans le cas où un emprunteur est listé à la rubrique 1 : Description n'apparaît pas dans la photo

Pour publier un prêt groupé, il est obligatoire d'inclure au moins un objet entreps (correspondant aux informations des entrepreneurs) dans le JSON.

## Paramètre : not_pictured

Le champ not_pictured est une liste de valeurs vraies ou fausses correspondant à l'ordre des emprunteurs énumérés à l'étape 1 : [l'étape 1 : Description - Prêts de groupe](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360031260191).

Par exemple, si le premier emprunteur listé à l'étape 1 n'est pas sur la photo, la première valeur de la liste not_pictured doit être true. Si le deuxième emprunteur mentionné à l'étape 1 apparaît sur la photo, la deuxième valeur de la liste not_pictured doit être false. Si le troisième emprunteur de l'étape 1 n'apparaît pas sur la photo, la troisième valeur de la liste not_pictured doit être vraie. Et ainsi de suite.
Lorsque vous fournissez des valeurs pour le champ not_pictured, vous devez le faire pour tous les emprunteurs énumérés à l'étape 1.

La photo de l'emprunteur peut être envoyée à PA2 avec l’API. Les tableaux de remboursement peuvent être envoyés dans plusieurs formats différents. Si vous pensez qu'un format différent de l'exemple ci-dessous conviendrait mieux à votre organisation, veuillez en informer Kiva et nous vous fournirons de plus amples informations. Pour vérifier si le document JSON que vous avez créé est correct, vous pouvez utiliser un validateur JSON en ligne comme celui-ci : https://jsonlint.com/ .

### Paramètres pour les projets de crédit

Pour créer un  projets de crédit entièrement remplie, plusieurs champs doivent être saisis avec des entrées définies dans le système Kiva. Chacun de ces identifiants peut être extrait par le biais des paramètres de l'API de Kiva. **Veuillez noter que ces APIs sont en mode lecture seule.**

Vous pouvez trouver des exemples d'appels à ces points de terminaison sur notre [collection Postman](https://github.com/kiva/fps-sdk/tree/main/samples/postman).

#### À partir de PA2 - Étape 1 : Description
* **1**: `location` - Ce champ doit être rempli avec une localité qui est actuellement définie sur le système Kiva. Les emplacements sont gérés dans la section Compte -> Localités sur PA2.
    * [GET Partner Locations](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/locationConfigsRouteUsingGET)
* **2**: `activity_id` - Ce champ doit contenir l'identifiant de l'activité de prêt.
    * [GET Loan Activities](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/activityConfigsRouteUsingGET)
* **3**: `description_language_id` - L'identifiant de la langue dans laquelle la description du prêt est rédigée.
    * [GET Locales](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/localeConfigsRouteUsingGET)

Lorsqu'ils sont saisis dans le projet de crédit, ces champs s'affichent sur PA2 comme suit :
![loandraft_step1.png](@site/static/img/pa2/loandraft_step1.png)

*Notez que le champ Secteur d'Activité Primaire est défini automatiquement à partir de l'Activité fournie.*

#### À partir de PA2 - Étape 2 : Modalités de Prêt
* **4**: `theme_type_id` - Ce champ doit contenir l'identifiant selon l'Initiative Spéciale. Il s'agit d'une classification plus large du prêt que l'Activité de Prêt, qui est pertinente pour les termes et conditions convenus par le partenaire.
    * [GET Loan Themes](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/themeConfigsRouteUsingGET)
* **5**: `currency` - Code ISO de la devise correspondant au montant demandé par l'emprunteur/euse.
    * [GET Locales](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/localeConfigsRouteUsingGET)

Lorsqu'ils sont renseignés dans Leprojets de crédit, ces champs s'affichent sur PA2 comme suit :
![loandraft_step2.png](@site/static/img/pa2/loandraft_step2.png)

## Documentation technique
Toda la documentación técnica de Kiva, incluidos los puntos finales, puede encontrarse aquí:
* Environnement de test (Stage) https://partnerapi.staging.kiva.org/swagger-ui/
* Production (à utiliser après les tests)  https://partnerapi.production.kiva.org/swagger-ui
