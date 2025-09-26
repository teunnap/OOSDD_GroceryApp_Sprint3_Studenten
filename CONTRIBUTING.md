# Git flow

Dit project maakt gebruik van Gitflow als branching-strategie.

* **Main branch**: Bevat een stabiele versie van het project en wordt alleen bijgewerkt met releases.
* **Develop branch**: Hierin wordt actieve ontwikkeling gedaan. Feature branches worden hieraan toegevoegd en samengevoegd.
* **Feature branches**: Nieuwe functionaliteiten worden ontwikkeld in feature branches die afgetakt zijn van `develop`.
* **Release branches**: Wanneer een nieuwe versie wordt voorbereid, wordt een release branch aangemaakt.
* **Hotfix branches**: Kritieke bugs in productie worden direct opgelost in een aparte hotfix-branch en direct naar `main` gemerged.
