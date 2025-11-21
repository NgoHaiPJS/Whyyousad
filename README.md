# WebApplication1

This repository contains a tiny static web page served from `wwwroot/sad.html`.

Goal: publish this site to GitHub Pages.

What I added
- `.gitignore` — common .NET ignores
- `.github/workflows/pages.yml` — GitHub Actions workflow that publishes `wwwroot` to GitHub Pages on `main` push.

How to push and publish (recommended, run locally in PowerShell):

1) Initialize git (if not already):

```powershell
cd 'C:\Game\C# Projects\WebApplication1'
git init
git add .
git commit -m "Initial commit"
```

2a) Create a GitHub repo and push using `gh` (recommended):

```powershell
gh repo create <your-username>/<repo-name> --public --source=. --remote=origin --push
```

2b) Or add a remote and push manually:

```powershell
git remote add origin https://github.com/<your-username>/<repo-name>.git
git branch -M main
git push -u origin main
```

3) The workflow will run on push to `main` and publish the files inside `wwwroot` to GitHub Pages. After the workflow completes, GitHub Pages settings will show the site URL (usually `https://<your-username>.github.io/<repo-name>/`).

If you want me to create the repo and push for you, tell me whether you'll provide a GitHub Personal Access Token (PAT) here or prefer to authorize via `gh auth login` locally — I will provide exact commands for either method.
