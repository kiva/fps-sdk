---
sidebar_position: 6
---

# Utilisation de l'API pour rapporter les remboursements
Nous recommandons à votre équipe technique de consulter le coordinateur Kiva de votre organisation afin de bien comprendre le processus de rapport de remboursements. Pour ce faire, demandez à votre équipe technique d'examiner les points suivants :
* [Cette vidéo](https://www.youtube.com/watch?v=pgg_rHBEQI8) explique en détail comment publier les rapports de remboursements.

## Processus  
1. Les informations sur les remboursements sont envoyées du SIG à PA2 en utilisant l'API.
2. La personne en charge des rapports de remboursements entre dans le PA2 où elle révise et finalise le rapport.

## Informations complémentaires

Pour les partenaires qui utilisent le format à deux colonnes pour les rapports de remboursements et le registre de suivi, seul l'ID du prêt (internal_loan_id) est nécessaire. Pour les partenaires utilisant le format à trois colonnes, l'ID du prêt (internal_loan_ID) et l'ID du client (internal_client_ID) sont tous deux requis. Cela peut être vérifié en allant dans PA2 -> Compte -> Profil -> Format de téléchargement CSV.  

Lorsque vous testez la connexion API pour les suivis, veuillez utiliser les informations d'un client/emprunteur réel qui ont été publiées dans Kiva. N'utilisez pas les mêmes informations client que celles utilisées pour publier un projet de prêt test, car PA2 n'enregistrera pas ce crédit comme un prêt actif pour lequel les remboursements doivent être rapportés.
  * Pour trouver un emprunteur Kiva existant, cliquez sur l'onglet "Remboursements" dans PA2, puis sur le nombre bleu de prêts avec des remboursements attendus. Cela vous permettra d'accéder à un rapport sur tous les clients Kiva pour lesquels Kiva attend un remboursement. Sélectionnez l'un des clients et les identifiants de prêt de ces clients pour les envoyer via l'API: ![repayments_expected.png](@site/static/img/repayments_expected.png)
  * Le montant déclaré doit correspondre à ce qui apparaît dans le SIG de votre organisation.
  * Le coordinateur Kiva devra encore se connecter à PA2 pour finaliser le rapport de remboursements.

Pour vérifier si le format JSON que vous avez créé est correct, vous pouvez utiliser un validateur JSON en ligne comme celui-ci : https://jsonlint.com/ .

## Documentation technique
* Toute la documentation technique de Kiva, y compris les points de terminaison, peut être trouvée ici :
  * Environnement de test (Stage) :https://partner-api-stage.kiva.org/swagger-ui/
  * Production (à utiliser après les tests)  https://partner-api.kiva.org/swagger-ui/
