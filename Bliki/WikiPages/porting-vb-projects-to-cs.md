<!-- TITLE: Porting VB Projects to C# -->
<!-- SUBTITLE: Words to the wise -->

# Introduction
If we had our drothers, we would prefer to have all of the projects in the master solution be C#.  It's a little more powerful, it's better supported, it's more popular, and it's easier to hire C# developers.  However, since so much of the code was written in VB.NET, prior to switching to C#, in 2016, it's not that easy.  It's been easy to say that all new projects added to the solution, since 2016, should be C#, but porting the existing projects has proven to be more difficult.  

Automatic conversion tools are handy, but they aren't perfect.  They are error prone and often produce ugly equivalents.  We aren't opposed to using them, but, if you do, you must carefully review the output, clean it up, and test it thoroughly.  Depending on the complexity of the VB code, the review and clean-up can sometimes take longer than rewriting it by hand.

While porting an existing project can be time consuming, what's potentially even more problematic is the merge conflicts that are introduced by doing so.  If any changes are being made to that project on any of the development branches, it will cause frustrating merge conflicts which are difficult to resolve and easy to mess up.

Since porting whole projects is so difficult, it's tempting to write new code in C#, even though the rest of the namespace in which the code resides is already in a VB project.  The problem with that, though, is then you end up with two projects for the same namespace, which can be confusing.  Not only that, but it's quite common for code within a namespace to be dependent on other code from the same namespace, so it's easy to end up in a situation where both projects need to reference each other, which is an invalid circular project reference.  For those reasons, we choose not to take this approach, except with unit test projects.  However, if the VB project does contain multiple namespaces, it's often possible to create namespace-specific C# projects for each of those namespaces, and only convert a portion of the project at a time.

Given the difficulties involved, it may be tempting to give up and just leave the existing projects in VB forever, resigning ourselves to writing new code for those namespaces in VB as well.  But in the imortal words of Mr. T, you've got to be somebody, or you'll be somebody's fool.  Foregoing failure as an option, the band-aid approach is often the best option left to us.

# Band-Aid Approach
1. Determine whether it makes sense to port only a single namespace, or a set of namespaces, or the full project
2. Notify the Puffins, making your intentions as clear as possible
3. Ask if anyone has any branches in which they've already made changes to the project-in-question.  If so, find out if they can merge those changes into the `master` branch before you begin the port.  If their changes can't be merged yet, find out if they are likely to be mergeable in an agreeable amount of time, so you can wait for that before starting the port.
4. Ask if anyone is likely to need to make changes to that project on their branches, so you can coordinate the port around their changes.
5. If all agree, take a branch from `master` and do the port on that branch as quickly as possible, making no other unrelated changes on that branch.  Once it's done, merge it back into `master`.  You know, like a band-aid.
6. Once you've merged it into `master`, notify the Puffins and ask them to merge `master` into all of their branches ASAP.