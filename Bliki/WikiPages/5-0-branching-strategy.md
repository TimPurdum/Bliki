<!-- TITLE: 5.0 Branching Strategy -->
<!-- SUBTITLE: A quick summary of 5.0 Branching Strategy for the master repository -->

# Branches
## Feature Branches
All large-scale work on new features (and even large bugs) should be done on a separately created feature branch. Only when this feature is considered complete and relatively bug-free in local testing should it be merged into master (checkout `master`, then use `git merge branch_name --squash`).
## Master Branch
The `master` branch of the `master` repository is the main "QA" and development branch. Thus, only code that is relatively bug-free from local testing should be merged here.
## Release_5_x
Release branches will be titled `release_5_x`, with the `x` being replaced by a version number (and eventually, someday, `release_6_x`). At the time a release branch is created, it should be in sync with `master`. However, once it is created and sent to customers, *only bug fixes* and special-circumstance features should be added. 

Adding bug fixes to a release branch means pushing them to master as discrete commits (i.e., *only* the bug in the commit), then, after QA testing, *cherry-picking* the commit to the release branch. `DbScript` changes should *not* be cherry-picked to release (see Database Scripts section below).

# Database Scripts
Unlike application code, the DbScripts in the `master` repository are designed to maintain a life-span history of all versions. Therefore, it is important to realize that if you add a DbScript into master with a current version number (e.g., `Upgrade IdnLawRms 5.0.45`), this will likely be used for customer updates. **No DbScripts should be pushed to Master without discussing with Allen or a senior dev**.

There are several strategies for working around this:
- Keep DbScript changes on feature branches as much as possible while working
- Whenever possible, make DbScripts backwards-compatible, so that they can be deployed independently of code changes (i.e., the db works with old code, but is changed to support new code)
- If a breaking change is necessary to the DbScripts, this should be given a new minor version number (e.g., `5.1` if current release is `5.0`). This will prevent it from being automatically deployed to customers when pushed to master for QA testing.