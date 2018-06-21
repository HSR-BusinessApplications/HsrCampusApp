# Contributing

When contributing to this repository, please first discuss the change you wish to make via issue or any other method with the owners of this repository before making a change.

## Overall Flow

1. A develop branch is created from master
2. Feature branches are created from develop
3. When a feature is complete it is merged into the develop branch
4. If an issue in master is detected a hotfix branch is created from master
5. Once the hotfix is complete it is merged to both develop and master


## Change Types
There are the following types of changes:

| Type | Description |
| --- | --- |
| Feature | Each new feature should reside in its own branch, which can be pushed to the central repository for backup/collaboration. But, instead of branching off of master, feature branches use develop as their parent branch. When a feature is complete, it gets merged back into develop. Features should never interact directly with master. |
| Hotfix | Maintenance or “hotfix” branches are used to quickly patch production releases. Hotfix branches are a lot like release branches and feature branches except they're based on master instead of develop. This is the only branch that should fork directly off of master. As soon as the fix is complete, it should be merged into both master and develop (or the current release branch), and master should be tagged with an updated version number. |


## Feature Process
To create a feature branch please use the following commands:

`git checkout develop`

`git checkout -b feature/your_feature_branch_name`

or with Gitflow:

`git flow feature start your_feature_branch_name`


When you're done with your branch you should follow the pull request process to request a merge into the develop branch.

## Hotfix Process
To create a hotfix branch please use the following commands:

`git checkout master`

`git checkout -b hotfix/your_hotfix_branch_name`

or with Gitflow:

`git flow hotfix start your_hotfix_branch_name`

When you're done with your branch you should follow the pull request process to request a merge into the develop and the master branch.


## Pull Request Process

1. Ensure that any unneeded install or build dependencies are removed before making the Pull Request.
2. If you installed a third party framework or library, ensure that the third party license is included in all the license file locations.
3. Ensure that the project builds successfully and there are no new warnings.
4. Ensure that all the tests run successfully.
5. The Pull Request will be reviewed by the project owners. If everything is OK the project owners will merge the Pull Request.
