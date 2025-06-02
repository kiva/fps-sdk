---
sidebar_position: 5
---

# Utilisation du point de terminaison d'emprunt de l'API


## Processus
* Une requête d'information est envoyée à PA2 via l'API, [GET /v3/partner/{id}/loans](https://partner-api-stage.kiva.org/swagger-ui/#/partners/loansRouteUsingGET).
* Il existe 4 paramètres de demande de recherche que vous pouvez envoyer :: query, status, offset, and limit
  * Query: peut être n'importe quelle ligne que vous tapez dans la barre de recherche PA2 (par exemple, le nom, l'ID du prêt, l'ID du client ou l'ID Kiva).
  * Status:  peut être l'un des éléments suivants :
    * refunded
    * inactive
    * inactive_expired
    * fundRaising
    * expired
    * raised
    * payingBack
    * ended
    * reviewed
    * issue
    * defaulted  

Pour vous assurer que tous les paiements sont rapportés lors de l'utilisation de l'option "GET", incluez tous les prêts avec le statut "PayingBack" et les prêts "defaulted". Les prêts dans ces états peuvent recevoir des paiements et doivent être **rapportés** à Kiva.

**Offset & limit (“offset”, “limit”):** fonctionne de la même manière que n'importe quelle pagination, offset est la distance à parcourir dans la liste et limit est le nombre de résultats à retourner. Ainsi, pour obtenir les 20 premières correspondances, il faudrait offset=0, limit=20 et les 20 suivantes seraient offset=20 limit=20, etc...  La valeur par défaut est offset=0 et limit=20.
* Nous ne recommandons pas de demander plus de 300, car le volume de données peut devenir très important et limiter les ressources du serveur.
* Une fois la demande effectuée, la réponse de l'API sera renvoyée au format JSON.

## Documentation technique
* Toute la documentation technique de Kiva, y compris les points de terminaison, peut être trouvée ici :
  * Environnement de test (stage): https://partnerapi.staging.kiva.org/swagger-ui/
  * Production (à utiliser après les tests) : https://partnerapi.production.kiva.org/swagger-ui