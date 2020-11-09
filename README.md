# makersofdenmark
Semester project for makers of denmark. 

## Start database
From Windows:
- go to database directory
- double click on `build.bat`
  * The Docker will download some file you need for the database to run
  * Let the thing work until its done
- double click on `run.bat`
  * first time it needs about 90 seconds to do its thing.
- Now you should be ready to run the solution.

## Migrations
From the `Package Manager Console` simply run:
- `Update-Database`

## git overview
#### General useful information 

- Check version installed: `git --version`
- Update to newest version on Windows: `git update-git-for-windows`
- Create empty Git repository in desired folder: `git init`
- Have git store/remember credentials and avoid repeating username/password prompts: `git config credential.helper store` (add `-global` after `config` to make it permanent across repos)

#### git commands for project
Command | Description
------- | -----------
`git add <file>` | Adds new/changed specified file to staging area
`git add .` | Adds all new/changed files to staging area
`git status` | Lists branch, untracked files and staged/unstaged files
`git commit -m "<message>"` |
`git diff` | See differences between file as of last commit and its current changes
`git remote add '<name>' '<url>'` | Connects to the remote repository that is located at the URL
`git remote -v` | Displays URL of your remote repository
`git push` |


#### Other general CLI tips
- Up- and down keys for commands that have previously been written
- Tab to finish command/folder-name etc.
