
# Dev Ops
## Serverless Framework
## AWS CodePipeLine
## GitHub Actions
GitHub Actions provide an easy way to automate software workflows. You can write actions yourself, but it is also possible to make use of actions written by other developers. Thousands of actions, able to be used for free, can be found on the [GitHub Marketplace](https://github.com/marketplace?type=actions).

Cloudbash makes use of GitHub Actions to build and deploy the documentation website to GitHub Pages, GitHub's free static web host. The action used is called [*actions-gh-page*s](https://github.com/peaceiris/actions-gh-pages) and was created by *peaceiris*.

GitHub actions are defined inside *workflows*, written in YAML files, and need to be placed in the *.github/workflows* folder at the root of the project. Workflows can be triggered after several different events; we have chosen to run this workflow after every push to the developer branch.


<<< @/../.github/workflows/build_docs.yml


<figure>
  <img src='../../assets/images/github_actions.png'>
  <figcaption>A few examples of successful executions of the GitHub Workflow (containing our Action).</figcaption>
</figure>

## Load Testing