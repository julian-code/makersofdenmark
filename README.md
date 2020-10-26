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
#### Quick general information

- Check version installed: `git --version`
- Update to newest version on Windows: `git update-git-for-windows`
- Create empty Git repository in desired folder: `git init`

#### git commands for project
Command | Description
------- | -----------
`git status` | Lists branch, untracked files and staged/unstaged files
`git add <file>` | Adds new/changed specified file to staging area
`git add .` | Adds all new/changed files to staging area

#### Other useful and general CLI tips
- Up- and down keys for commands that have previously been written
- Tab to finish command/folder-name etc.
