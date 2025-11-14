# Explication des patterns utilisés

## Contexte et intention

Je développe un petit jeu console où deux joueurs s'affrontent à tour de role. Je veux :

- Afficher l'état (barres de vie) et déclencher un "Game Over" sans coupler fortement ces comportements au joueur.
- Composer des armes/défenses avec des améliorations empilables (sans multiplier les sous-classes ni écrire des blocs `if/else` partout).

Pour cela, j'utilise deux patterns : **Observer** et **Decorator**.

---

## Pattern Observer

**Intention**

- Découpler la source d'événements (le joueur qui perd des points de vie) des réactions (mise à jour de l'UI, détection de fin de partie).

**Rôles dans mon code**

- Sujet (Subject) : `Player` (`classes/Player.cs`)
  - Interface : `ISubject` (`interfaces/ISubject.cs`)
  - Méthodes clés : `Attach`, `Detach`, `Notify`
- Observateurs (Observers) :
  - `HealthBar` (`classes/Observers/HealthBar.cs`) — met à jour l'affichage de la vie.
  - `GameOverManager` (`classes/GameOverManager.cs`) — détecte la mort et signale la fin de partie.
  - Interface : `IObserver` (`interfaces/IObserver.cs`) — méthode `Update(...)` appelée quand `Player` notifie.

**Câblage**

- Dans `GameManager` (`classes/GameManager.cs`), j'attache les observateurs à chaque joueur :
  - `player.Attach(new HealthBar())`
  - `player.Attach(new GameOverManager())`
- Quand un joueur subit des dégâts (`Player.TakesDamage(...)`), le `Player` met à jour `HealthPoints` puis appelle `Notify(delta)`.
- Les observateurs reçoivent `Update(...)` et réagissent :
  - `HealthBar` affiche la nouvelle vie.
  - `GameOverManager` détecte si `currentHealth <= 0` et déclenche l'état "game over" (dans ma version, je définis un flag et laisse `GameManager` gérer le redémarrage sur la touche Espace).

**Pourquoi Observer ici**

- Le `Player` ne connaît pas la "UI" ni la logique de fin de partie.
- Je peux ajouter/enlever des comportements en branchant/débranchant des observateurs, sans toucher à `Player`.

**Bénéfices**

- Faible couplage et forte cohésion.
- Extensible : je peux ajouter d'autres comportements sans modifier `Player`.

---

## Pattern Decorator

**Intention**

- Ajouter des fonctionnalités à une arme/défense dynamiquement et de façon empilable, sans créer une explosion de sous-classes.
- Respecter l'OCP : je peux introduire de nouveaux bonus sans modifier les classes existantes.

**Rôles dans mon code**

- Composant de base (abstraction) :
  - Armes : `Weapon` (`classes/Weapon.cs`)
  - Défenses : `Defense` (`classes/Defense.cs` ou équivalent dans `classes/Defenses`)
- Implémentations concrètes :
  - Ex. armes : `Katana`, `Nunchaku`, `Shuriken`
  - Ex. défenses : `Shield`, `SmokeBomb`, `Roll`
- Décorateurs :
  - Base : `WeaponDecorator` (`classes/Weapons/Decorators/WeaponDecorator.cs`) et `DefenseDecorator` (`classes/Defenses/Decorators/DefenseDecorator.cs`).
  - Exemples d'upgrades armes : `SharpnessDecorator`, `MomentumDecorator`.
  - Exemples d'upgrades défenses : `QuickReflexeDecorator`, `DiamondDecorator`.

**Comment je compose**

- J'équipe une arme de base, puis je la "wrap" avec des décorateurs :
  - Exemple : `new MomentumDecorator(new SharpnessDecorator(new Katana()))`
- `Player.EquipWeapon(...)` reçoit l'objet déjà décoré, et `Player.Attack(...)` délègue à `Weapon.Attack()` / `Weapon.AttackSound()` — le `Player` n'a aucune connaissance des décorateurs concrets.
- Idem pour les défenses via `Player.EquipDefense(...)` et `Defense.Defend()`.

**Pourquoi Decorator ici**

- Je veux pouvoir combiner librement les bonus (tranchant + momentum + élément, etc.) sans créer des sous-classes pour chaque combinaison.
- Je peux empiler, enlever, réordonner des effets au runtime.

**Bénéfices**

- Composition flexible et runtime-friendly.
- Ouvert aux ajouts de nouveaux effets, fermé aux modifications des classes existantes.