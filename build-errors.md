# Build Failure Summary

Branch: claude/compare-yfinance-features-hBw3G
Commit: d8650052f7ff2fd444b6583803a0c6d3df6031ed
Workflow: CI
Failed Jobs: 3

## Failed Job: Build and Test

```
2025-12-30T06:02:48.8367615Z Current runner version: '2.330.0'
2025-12-30T06:02:48.8392113Z ##[group]Runner Image Provisioner
2025-12-30T06:02:48.8392978Z Hosted Compute Agent
2025-12-30T06:02:48.8393528Z Version: 20251211.462
2025-12-30T06:02:48.8394117Z Commit: 6cbad8c2bb55d58165063d031ccabf57e2d2db61
2025-12-30T06:02:48.8395060Z Build Date: 2025-12-11T16:28:49Z
2025-12-30T06:02:48.8396020Z Worker ID: {2b179c06-d0de-480a-9449-c25711f5ea82}
2025-12-30T06:02:48.8396702Z ##[endgroup]
2025-12-30T06:02:48.8397209Z ##[group]Operating System
2025-12-30T06:02:48.8397824Z Ubuntu
2025-12-30T06:02:48.8398289Z 24.04.3
2025-12-30T06:02:48.8398975Z LTS
2025-12-30T06:02:48.8399509Z ##[endgroup]
2025-12-30T06:02:48.8400030Z ##[group]Runner Image
2025-12-30T06:02:48.8400533Z Image: ubuntu-24.04
2025-12-30T06:02:48.8401082Z Version: 20251215.174.1
2025-12-30T06:02:48.8402030Z Included Software: https://github.com/actions/runner-images/blob/ubuntu24/20251215.174/images/ubuntu/Ubuntu2404-Readme.md
2025-12-30T06:02:48.8403592Z Image Release: https://github.com/actions/runner-images/releases/tag/ubuntu24%2F20251215.174
2025-12-30T06:02:48.8404909Z ##[endgroup]
2025-12-30T06:02:48.8406239Z ##[group]GITHUB_TOKEN Permissions
2025-12-30T06:02:48.8408090Z Checks: write
2025-12-30T06:02:48.8408613Z Contents: read
2025-12-30T06:02:48.8409169Z Metadata: read
2025-12-30T06:02:48.8409719Z PullRequests: write
2025-12-30T06:02:48.8410191Z Statuses: write
2025-12-30T06:02:48.8410710Z ##[endgroup]
2025-12-30T06:02:48.8413101Z Secret source: Actions
2025-12-30T06:02:48.8413852Z Prepare workflow directory
2025-12-30T06:02:48.8759672Z Prepare all required actions
2025-12-30T06:02:48.8798995Z Getting action download info
2025-12-30T06:02:49.2336609Z Download action repository 'actions/checkout@v4' (SHA:34e114876b0b11c390a56381ad16ebd13914f8d5)
2025-12-30T06:02:49.3271738Z Download action repository 'actions/setup-dotnet@v4' (SHA:67a3573c9a986a3f9c594539f4ab511d57bb3ce9)
2025-12-30T06:02:49.7076006Z Download action repository 'actions/upload-artifact@v4' (SHA:ea165f8d65b6e75b540449e92b4886f43607fa02)
2025-12-30T06:02:49.8285580Z Download action repository 'dorny/test-reporter@v1' (SHA:d61b558e8df85cb60d09ca3e5b09653b4477cea7)
2025-12-30T06:02:50.3635702Z Complete job name: Build and Test
2025-12-30T06:02:50.4486247Z ##[group]Run actions/checkout@v4
2025-12-30T06:02:50.4487534Z with:
2025-12-30T06:02:50.4488406Z   repository: CalvinPangch/YFinance.NET
2025-12-30T06:02:50.4489729Z   token: ***
2025-12-30T06:02:50.4490443Z   ssh-strict: true
2025-12-30T06:02:50.4491241Z   ssh-user: git
2025-12-30T06:02:50.4492057Z   persist-credentials: true
2025-12-30T06:02:50.4492920Z   clean: true
2025-12-30T06:02:50.4493686Z   sparse-checkout-cone-mode: true
2025-12-30T06:02:50.4495009Z   fetch-depth: 1
2025-12-30T06:02:50.4495781Z   fetch-tags: false
2025-12-30T06:02:50.4496549Z   show-progress: true
2025-12-30T06:02:50.4497334Z   lfs: false
2025-12-30T06:02:50.4498043Z   submodules: false
2025-12-30T06:02:50.4498830Z   set-safe-directory: true
2025-12-30T06:02:50.4499946Z ##[endgroup]
2025-12-30T06:02:50.5637996Z Syncing repository: CalvinPangch/YFinance.NET
2025-12-30T06:02:50.5642278Z ##[group]Getting Git version info
2025-12-30T06:02:50.5645090Z Working directory is '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T06:02:50.5647381Z [command]/usr/bin/git version
2025-12-30T06:02:50.5718351Z git version 2.52.0
2025-12-30T06:02:50.5746026Z ##[endgroup]
2025-12-30T06:02:50.5762930Z Temporarily overriding HOME='/home/runner/work/_temp/562265c1-6bf4-42d1-82ca-67344296d153' before making global git config changes
2025-12-30T06:02:50.5767830Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T06:02:50.5771747Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T06:02:50.5810708Z Deleting the contents of '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T06:02:50.5814531Z ##[group]Initializing the repository
2025-12-30T06:02:50.5819570Z [command]/usr/bin/git init /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T06:02:50.5921703Z hint: Using 'master' as the name for the initial branch. This default branch name
2025-12-30T06:02:50.5924512Z hint: will change to "main" in Git 3.0. To configure the initial branch name
2025-12-30T06:02:50.5927721Z hint: to use in all of your new repositories, which will suppress this warning,
2025-12-30T06:02:50.5930246Z hint: call:
2025-12-30T06:02:50.5931490Z hint:
2025-12-30T06:02:50.5933066Z hint: 	git config --global init.defaultBranch <name>
2025-12-30T06:02:50.5935202Z hint:
2025-12-30T06:02:50.5937088Z hint: Names commonly chosen instead of 'master' are 'main', 'trunk' and
2025-12-30T06:02:50.5940232Z hint: 'development'. The just-created branch can be renamed via this command:
2025-12-30T06:02:50.5942614Z hint:
2025-12-30T06:02:50.5943795Z hint: 	git branch -m <name>
2025-12-30T06:02:50.5945381Z hint:
2025-12-30T06:02:50.5946754Z hint: Disable this message with "git config set advice.defaultBranchName false"
2025-12-30T06:02:50.5948738Z Initialized empty Git repository in /home/runner/work/YFinance.NET/YFinance.NET/.git/
2025-12-30T06:02:50.5952916Z [command]/usr/bin/git remote add origin https://github.com/CalvinPangch/YFinance.NET
2025-12-30T06:02:50.5975325Z ##[endgroup]
2025-12-30T06:02:50.5977713Z ##[group]Disabling automatic garbage collection
2025-12-30T06:02:50.5979882Z [command]/usr/bin/git config --local gc.auto 0
2025-12-30T06:02:50.6009481Z ##[endgroup]
2025-12-30T06:02:50.6011697Z ##[group]Setting up auth
2025-12-30T06:02:50.6017138Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T06:02:50.6049912Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T06:02:50.6380843Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T06:02:50.6411224Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T06:02:50.6641371Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T06:02:50.6675142Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T06:02:50.6895347Z [command]/usr/bin/git config --local http.https://github.com/.extraheader AUTHORIZATION: basic ***
2025-12-30T06:02:50.6929690Z ##[endgroup]
2025-12-30T06:02:50.6932031Z ##[group]Fetching the repository
2025-12-30T06:02:50.6941032Z [command]/usr/bin/git -c protocol.version=2 fetch --no-tags --prune --no-recurse-submodules --depth=1 origin +149da10551655e08884fd10692b7cd2d7cc993df:refs/remotes/pull/7/merge
2025-12-30T06:02:50.9555717Z From https://github.com/CalvinPangch/YFinance.NET
2025-12-30T06:02:50.9559530Z  * [new ref]         149da10551655e08884fd10692b7cd2d7cc993df -> pull/7/merge
2025-12-30T06:02:50.9602411Z ##[endgroup]
2025-12-30T06:02:50.9604925Z ##[group]Determining the checkout info
2025-12-30T06:02:50.9607395Z ##[endgroup]
2025-12-30T06:02:50.9608797Z [command]/usr/bin/git sparse-checkout disable
2025-12-30T06:02:50.9657463Z [command]/usr/bin/git config --local --unset-all extensions.worktreeConfig
2025-12-30T06:02:50.9686066Z ##[group]Checking out the ref
2025-12-30T06:02:50.9689360Z [command]/usr/bin/git checkout --progress --force refs/remotes/pull/7/merge
2025-12-30T06:02:50.9809661Z Note: switching to 'refs/remotes/pull/7/merge'.
2025-12-30T06:02:50.9810711Z 
2025-12-30T06:02:50.9811641Z You are in 'detached HEAD' state. You can look around, make experimental
2025-12-30T06:02:50.9813701Z changes and commit them, and you can discard any commits you make in this
2025-12-30T06:02:50.9816815Z state without impacting any branches by switching back to a branch.
2025-12-30T06:02:50.9818573Z 
2025-12-30T06:02:50.9819692Z If you want to create a new branch to retain commits you create, you may
2025-12-30T06:02:50.9822769Z do so (now or later) by using -c with the switch command. Example:
2025-12-30T06:02:50.9824622Z 
2025-12-30T06:02:50.9825282Z   git switch -c <new-branch-name>
2025-12-30T06:02:50.9826314Z 
2025-12-30T06:02:50.9826868Z Or undo this operation with:
2025-12-30T06:02:50.9827807Z 
2025-12-30T06:02:50.9828297Z   git switch -
2025-12-30T06:02:50.9829004Z 
2025-12-30T06:02:50.9830095Z Turn off this advice by setting config variable advice.detachedHead to false
2025-12-30T06:02:50.9831191Z 
2025-12-30T06:02:50.9832368Z HEAD is now at 149da10 Merge d8650052f7ff2fd444b6583803a0c6d3df6031ed into 71fb9da4e8d5107b786fc55175e6a45ef90f93e3
2025-12-30T06:02:50.9836890Z ##[endgroup]
2025-12-30T06:02:50.9856305Z [command]/usr/bin/git log -1 --format=%H
2025-12-30T06:02:50.9878282Z 149da10551655e08884fd10692b7cd2d7cc993df
2025-12-30T06:02:51.0279903Z ##[group]Run actions/setup-dotnet@v4
2025-12-30T06:02:51.0280862Z with:
2025-12-30T06:02:51.0281513Z   dotnet-version: 10.0.x
2025-12-30T06:02:51.0282314Z   cache: false
2025-12-30T06:02:51.0282972Z ##[endgroup]
2025-12-30T06:02:51.2048102Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --runtime dotnet --channel LTS
2025-12-30T06:02:51.4753626Z dotnet-install: .NET Core Runtime with version '10.0.1' is already installed.
2025-12-30T06:02:51.4784851Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --channel 10.0
2025-12-30T06:02:51.7278326Z dotnet-install: .NET Core SDK with version '10.0.101' is already installed.
2025-12-30T06:02:51.7445229Z ##[group]Run dotnet restore
2025-12-30T06:02:51.7446234Z [36;1mdotnet restore[0m
2025-12-30T06:02:51.7487617Z shell: /usr/bin/bash -e {0}
2025-12-30T06:02:51.7488472Z env:
2025-12-30T06:02:51.7489150Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T06:02:51.7490016Z ##[endgroup]
2025-12-30T06:03:03.3980024Z   Determining projects to restore...
2025-12-30T06:03:05.5324038Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj (in 516 ms).
2025-12-30T06:03:05.5326189Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/YFinance.NET.Models.csproj (in 516 ms).
2025-12-30T06:03:09.0578707Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj (in 4.11 sec).
2025-12-30T06:03:09.1151478Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj (in 4.17 sec).
2025-12-30T06:03:09.1591742Z ##[group]Run dotnet build --configuration Release --no-restore
2025-12-30T06:03:09.1592228Z [36;1mdotnet build --configuration Release --no-restore[0m
2025-12-30T06:03:09.1624565Z shell: /usr/bin/bash -e {0}
2025-12-30T06:03:09.1624812Z env:
2025-12-30T06:03:09.1624999Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T06:03:09.1625239Z ##[endgroup]
2025-12-30T06:03:18.2504944Z   YFinance.NET.Models -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/bin/Release/net10.0/YFinance.NET.Models.dll
2025-12-30T06:03:18.7040035Z   YFinance.NET.Interfaces -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/bin/Release/net10.0/YFinance.NET.Interfaces.dll
2025-12-30T06:03:20.2087321Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2109669Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2116614Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2122485Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2128582Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2134703Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2139737Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2145118Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2148082Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2151125Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2275450Z 
2025-12-30T06:03:20.2275830Z Build FAILED.
2025-12-30T06:03:20.2276101Z 
2025-12-30T06:03:20.2279039Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2284783Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2288208Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2291061Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2293814Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2298647Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2302100Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2305778Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2309294Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2312769Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:20.2314809Z     5 Warning(s)
2025-12-30T06:03:20.2315020Z     5 Error(s)
2025-12-30T06:03:20.2315163Z 
2025-12-30T06:03:20.2315294Z Time Elapsed 00:00:10.71
2025-12-30T06:03:20.2770188Z ##[error]Process completed with exit code 1.
2025-12-30T06:03:20.2851932Z ##[group]Run actions/upload-artifact@v4
2025-12-30T06:03:20.2852239Z with:
2025-12-30T06:03:20.2852417Z   name: test-results
2025-12-30T06:03:20.2852643Z   path: **/TestResults/*.trx

2025-12-30T06:03:20.2852877Z   retention-days: 30
2025-12-30T06:03:20.2853099Z   if-no-files-found: warn
2025-12-30T06:03:20.2853322Z   compression-level: 6
2025-12-30T06:03:20.2853524Z   overwrite: false
2025-12-30T06:03:20.2853729Z   include-hidden-files: false
2025-12-30T06:03:20.2853958Z env:
2025-12-30T06:03:20.2854142Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T06:03:20.2854623Z ##[endgroup]
2025-12-30T06:03:20.6214596Z ##[warning]No files were found with the provided path: **/TestResults/*.trx. No artifacts will be uploaded.
2025-12-30T06:03:20.6399872Z ##[group]Run dorny/test-reporter@v1
2025-12-30T06:03:20.6400360Z with:
2025-12-30T06:03:20.6400699Z   name: Test Results
2025-12-30T06:03:20.6401096Z   path: **/TestResults/*.trx
2025-12-30T06:03:20.6401505Z   reporter: dotnet-trx
2025-12-30T06:03:20.6401894Z   fail-on-error: false
2025-12-30T06:03:20.6402282Z   path-replace-backslashes: false
2025-12-30T06:03:20.6402688Z   list-suites: all
2025-12-30T06:03:20.6402993Z   list-tests: all
2025-12-30T06:03:20.6403323Z   max-annotations: 10
2025-12-30T06:03:20.6403670Z   fail-on-empty: true
2025-12-30T06:03:20.6404030Z   only-summary: false
2025-12-30T06:03:20.6404789Z   token: ***
2025-12-30T06:03:20.6405093Z env:
2025-12-30T06:03:20.6405387Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T06:03:20.6405758Z ##[endgroup]
2025-12-30T06:03:20.7936626Z Action was triggered by pull_request: using SHA from head of source branch
2025-12-30T06:03:20.7937804Z Check runs will be created with SHA=d8650052f7ff2fd444b6583803a0c6d3df6031ed
2025-12-30T06:03:20.7939628Z ##[group]Listing all files tracked by git
2025-12-30T06:03:20.7971741Z [command]/usr/bin/git ls-files -z
2025-12-30T06:03:20.8119161Z .github/workflows/ci.yml .github/workflows/claude-auto-fix.yml .github/workflows/claude-code-review.yml .github/workflows/claude.yml .gitignore CLAUDE.md PR_DESCRIPTION.md README.md SECURITY_FIX_SUMMARY.md YFinance.NET.Implementation/CalendarService.cs YFinance.NET.Implementation/Constants/YahooFinanceConstants.cs YFinance.NET.Implementation/DependencyInjection/ServiceCollectionExtensions.cs YFinance.NET.Implementation/DomainService.cs YFinance.NET.Implementation/IsinService.cs YFinance.NET.Implementation/LiveMarketService.cs YFinance.NET.Implementation/MarketService.cs YFinance.NET.Implementation/MultiTickerService.cs YFinance.NET.Implementation/Properties/AssemblyInfo.cs YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs YFinance.NET.Implementation/Scrapers/CalendarScraper.cs YFinance.NET.Implementation/Scrapers/CalendarVisualizationScraper.cs YFinance.NET.Implementation/Scrapers/EarningsScraper.cs YFinance.NET.Implementation/Scrapers/EsgScraper.cs YFinance.NET.Implementation/Scrapers/FastInfoScraper.cs YFinance.NET.Implementation/Scrapers/FundamentalsScraper.cs YFinance.NET.Implementation/Scrapers/FundsScraper.cs YFinance.NET.Implementation/Scrapers/HistoryScraper.cs YFinance.NET.Implementation/Scrapers/HoldersScraper.cs YFinance.NET.Implementation/Scrapers/InfoScraper.cs YFinance.NET.Implementation/Scrapers/LookupScraper.cs YFinance.NET.Implementation/Scrapers/NewsScraper.cs YFinance.NET.Implementation/Scrapers/OptionsScraper.cs YFinance.NET.Implementation/Scrapers/QuoteScraper.cs YFinance.NET.Implementation/Scrapers/ScreenerScraper.cs YFinance.NET.Implementation/Scrapers/SearchScraper.cs YFinance.NET.Implementation/Scrapers/SharesScraper.cs YFinance.NET.Implementation/Services/CacheService.cs YFinance.NET.Implementation/Services/CookieService.cs YFinance.NET.Implementation/Services/RateLimitService.cs YFinance.NET.Implementation/TickerService.cs YFinance.NET.Implementation/Tickers.cs YFinance.NET.Implementation/Utils/DataParser.cs YFinance.NET.Implementation/Utils/JsonElementExtensions.cs YFinance.NET.Implementation/Utils/PriceRepair.cs YFinance.NET.Implementation/Utils/SymbolValidator.cs YFinance.NET.Implementation/Utils/TimezoneHelper.cs YFinance.NET.Implementation/YFinance.NET.Implementation.csproj YFinance.NET.Implementation/YahooFinanceClient.cs YFinance.NET.Interfaces/ICalendarService.cs YFinance.NET.Interfaces/IDomainService.cs YFinance.NET.Interfaces/IIsinService.cs YFinance.NET.Interfaces/ILiveMarketService.cs YFinance.NET.Interfaces/IMarketService.cs YFinance.NET.Interfaces/IMultiTickerService.cs YFinance.NET.Interfaces/ITickerService.cs YFinance.NET.Interfaces/IYahooFinanceClient.cs YFinance.NET.Interfaces/Scrapers/IAnalysisScraper.cs YFinance.NET.Interfaces/Scrapers/ICalendarScraper.cs YFinance.NET.Interfaces/Scrapers/ICalendarVisualizationScraper.cs YFinance.NET.Interfaces/Scrapers/IEarningsScraper.cs YFinance.NET.Interfaces/Scrapers/IEsgScraper.cs YFinance.NET.Interfaces/Scrapers/IFastInfoScraper.cs YFinance.NET.Interfaces/Scrapers/IFundamentalsScraper.cs YFinance.NET.Interfaces/Scrapers/IFundsScraper.cs YFinance.NET.Interfaces/Scrapers/IHistoryScraper.cs YFinance.NET.Interfaces/Scrapers/IHoldersScraper.cs YFinance.NET.Interfaces/Scrapers/IInfoScraper.cs YFinance.NET.Interfaces/Scrapers/ILookupScraper.cs YFinance.NET.Interfaces/Scrapers/INewsScraper.cs YFinance.NET.Interfaces/Scrapers/IOptionsScraper.cs YFinance.NET.Interfaces/Scrapers/IQuoteScraper.cs YFinance.NET.Interfaces/Scrapers/IScreenerScraper.cs YFinance.NET.Interfaces/Scrapers/ISearchScraper.cs YFinance.NET.Interfaces/Scrapers/ISharesScraper.cs YFinance.NET.Interfaces/Services/ICacheService.cs YFinance.NET.Interfaces/Services/ICookieService.cs YFinance.NET.Interfaces/Services/IRateLimitService.cs YFinance.NET.Interfaces/Utils/IDataParser.cs YFinance.NET.Interfaces/Utils/IPriceRepair.cs YFinance.NET.Interfaces/Utils/ISymbolValidator.cs YFinance.NET.Interfaces/Utils/ITimezoneHelper.cs YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj YFinance.NET.Models/ActionData.cs YFinance.NET.Models/ActionsData.cs YFinance.NET.Models/AnalystData.cs YFinance.NET.Models/CalendarData.cs YFinance.NET.Models/CalendarQuery.cs YFinance.NET.Models/CalendarRequest.cs YFinance.NET.Models/CalendarResult.cs YFinance.NET.Models/DomainData.cs YFinance.NET.Models/EarningsData.cs YFinance.NET.Models/Enums/Interval.cs YFinance.NET.Models/Enums/LookupType.cs YFinance.NET.Models/Enums/Period.cs YFinance.NET.Models/Enums/StatementType.cs YFinance.NET.Models/EsgData.cs YFinance.NET.Models/Exceptions/DataParsingException.cs YFinance.NET.Models/Exceptions/InvalidTickerException.cs YFinance.NET.Models/Exceptions/RateLimitException.cs YFinance.NET.Models/Exceptions/YahooFinanceException.cs YFinance.NET.Models/FastInfo.cs YFinance.NET.Models/FastInfoData.cs YFinance.NET.Models/FinancialStatement.cs YFinance.NET.Models/FundsData.cs YFinance.NET.Models/HistoricalData.cs YFinance.NET.Models/HistoryMetadata.cs YFinance.NET.Models/HolderData.cs YFinance.NET.Models/InfoData.cs YFinance.NET.Models/LivePriceData.cs YFinance.NET.Models/LookupResult.cs YFinance.NET.Models/MajorHoldersData.cs YFinance.NET.Models/NewsItem.cs YFinance.NET.Models/OptionsData.cs YFinance.NET.Models/QuoteData.cs YFinance.NET.Models/RecommendationData.cs YFinance.NET.Models/RecommendationsSummaryData.cs YFinance.NET.Models/Requests/ActionsRequest.cs YFinance.NET.Models/Requests/EarningsDatesRequest.cs YFinance.NET.Models/Requests/HistoryRequest.cs YFinance.NET.Models/Requests/LookupRequest.cs YFinance.NET.Models/Requests/NewsRequest.cs YFinance.NET.Models/Requests/OptionChainRequest.cs YFinance.NET.Models/Requests/ScreenerRequest.cs YFinance.NET.Models/Requests/SearchRequest.cs YFinance.NET.Models/Requests/SharesHistoryRequest.cs YFinance.NET.Models/ScreenerPredefinedQueries.cs YFinance.NET.Models/ScreenerQuery.cs YFinance.NET.Models/ScreenerResult.cs YFinance.NET.Models/SearchResult.cs YFinance.NET.Models/SharesData.cs YFinance.NET.Models/YFinance.NET.Models.csproj YFinance.NET.Tests/Integration/TickerServiceIntegrationTests.cs YFinance.NET.Tests/TestFixtures/MockHttpMessageHandler.cs YFinance.NET.Tests/TestFixtures/MockYahooFinanceClient.cs YFinance.NET.Tests/TestFixtures/TestDataBuilder.cs YFinance.NET.Tests/Unit/MultiTickerServiceTests.cs YFinance.NET.Tests/Unit/Scrapers/AnalysisScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/CalendarScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/EarningsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/EsgScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/FundamentalsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/FundsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/HoldersScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/LookupScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/NewsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/OptionsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/ScreenerScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/SearchScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/SharesScraperTests.cs YFinance.NET.Tests/Unit/Services/RateLimitServiceTests.cs YFinance.NET.Tests/Unit/TickerServiceTests.cs YFinance.NET.Tests/Unit/Utils/DataParserTests.cs YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs YFinance.NET.Tests/Unit/YahooFinanceClientTests.cs YFinance.NET.Tests/YFinance.NET.Tests.csproj YFinance.NET.sln build-errors.md build.log git-push-retry.sh global.json 
2025-12-30T06:03:20.8176512Z ##[endgroup]
2025-12-30T06:03:20.8176994Z Found 162 files tracked by GitHub
2025-12-30T06:03:20.8177546Z Using test report parser 'dotnet-trx'
2025-12-30T06:03:20.8221181Z ##[group]Creating test report Test Results
2025-12-30T06:03:20.8228183Z ##[warning]No file matches path **/TestResults/*.trx
2025-12-30T06:03:20.8229854Z ##[endgroup]
2025-12-30T06:03:20.8238650Z ##[error]No test report files were found
2025-12-30T06:03:20.8371935Z Post job cleanup.
2025-12-30T06:03:20.9365072Z [command]/usr/bin/git version
2025-12-30T06:03:20.9442035Z git version 2.52.0
2025-12-30T06:03:20.9509118Z Temporarily overriding HOME='/home/runner/work/_temp/7afc89b5-0f15-49e0-82b0-ca58c06d8884' before making global git config changes
2025-12-30T06:03:20.9511230Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T06:03:20.9517958Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T06:03:20.9578552Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T06:03:20.9622591Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T06:03:20.9886757Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T06:03:20.9912705Z http.https://github.com/.extraheader
2025-12-30T06:03:20.9927518Z [command]/usr/bin/git config --local --unset-all http.https://github.com/.extraheader
2025-12-30T06:03:20.9964401Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T06:03:21.0215845Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T06:03:21.0252382Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T06:03:21.0740158Z Cleaning up orphan processes
2025-12-30T06:03:21.1077573Z Terminate orphan process: pid (2345) (dotnet)
2025-12-30T06:03:21.1097415Z Terminate orphan process: pid (2346) (dotnet)
2025-12-30T06:03:21.1116787Z Terminate orphan process: pid (2347) (dotnet)
2025-12-30T06:03:21.1145494Z Terminate orphan process: pid (2468) (VBCSCompiler)

```

## Failed Job: Code Quality Check

```
2025-12-30T06:02:52.2470241Z Current runner version: '2.330.0'
2025-12-30T06:02:52.2493788Z ##[group]Runner Image Provisioner
2025-12-30T06:02:52.2494683Z Hosted Compute Agent
2025-12-30T06:02:52.2495205Z Version: 20251211.462
2025-12-30T06:02:52.2496072Z Commit: 6cbad8c2bb55d58165063d031ccabf57e2d2db61
2025-12-30T06:02:52.2496883Z Build Date: 2025-12-11T16:28:49Z
2025-12-30T06:02:52.2497560Z Worker ID: {afc05717-3e2a-447b-a36d-4ba0cdbe8dc7}
2025-12-30T06:02:52.2498216Z ##[endgroup]
2025-12-30T06:02:52.2498768Z ##[group]Operating System
2025-12-30T06:02:52.2499304Z Ubuntu
2025-12-30T06:02:52.2499772Z 24.04.3
2025-12-30T06:02:52.2500247Z LTS
2025-12-30T06:02:52.2500680Z ##[endgroup]
2025-12-30T06:02:52.2501195Z ##[group]Runner Image
2025-12-30T06:02:52.2501663Z Image: ubuntu-24.04
2025-12-30T06:02:52.2502232Z Version: 20251215.174.1
2025-12-30T06:02:52.2503190Z Included Software: https://github.com/actions/runner-images/blob/ubuntu24/20251215.174/images/ubuntu/Ubuntu2404-Readme.md
2025-12-30T06:02:52.2504761Z Image Release: https://github.com/actions/runner-images/releases/tag/ubuntu24%2F20251215.174
2025-12-30T06:02:52.2505749Z ##[endgroup]
2025-12-30T06:02:52.2507390Z ##[group]GITHUB_TOKEN Permissions
2025-12-30T06:02:52.2509252Z Checks: write
2025-12-30T06:02:52.2509820Z Contents: read
2025-12-30T06:02:52.2510398Z Metadata: read
2025-12-30T06:02:52.2510941Z PullRequests: write
2025-12-30T06:02:52.2511409Z Statuses: write
2025-12-30T06:02:52.2511966Z ##[endgroup]
2025-12-30T06:02:52.2514327Z Secret source: Actions
2025-12-30T06:02:52.2515019Z Prepare workflow directory
2025-12-30T06:02:52.2848057Z Prepare all required actions
2025-12-30T06:02:52.2885535Z Getting action download info
2025-12-30T06:02:52.6621590Z Download action repository 'actions/checkout@v4' (SHA:34e114876b0b11c390a56381ad16ebd13914f8d5)
2025-12-30T06:02:52.7814770Z Download action repository 'actions/setup-dotnet@v4' (SHA:67a3573c9a986a3f9c594539f4ab511d57bb3ce9)
2025-12-30T06:02:53.5702686Z Complete job name: Code Quality Check
2025-12-30T06:02:53.6546831Z ##[group]Run actions/checkout@v4
2025-12-30T06:02:53.6548226Z with:
2025-12-30T06:02:53.6548969Z   repository: CalvinPangch/YFinance.NET
2025-12-30T06:02:53.6550276Z   token: ***
2025-12-30T06:02:53.6550971Z   ssh-strict: true
2025-12-30T06:02:53.6551688Z   ssh-user: git
2025-12-30T06:02:53.6552420Z   persist-credentials: true
2025-12-30T06:02:53.6553258Z   clean: true
2025-12-30T06:02:53.6553987Z   sparse-checkout-cone-mode: true
2025-12-30T06:02:53.6554913Z   fetch-depth: 1
2025-12-30T06:02:53.6555619Z   fetch-tags: false
2025-12-30T06:02:53.6556500Z   show-progress: true
2025-12-30T06:02:53.6557247Z   lfs: false
2025-12-30T06:02:53.6557925Z   submodules: false
2025-12-30T06:02:53.6558687Z   set-safe-directory: true
2025-12-30T06:02:53.6559762Z ##[endgroup]
2025-12-30T06:02:53.7685616Z Syncing repository: CalvinPangch/YFinance.NET
2025-12-30T06:02:53.7690011Z ##[group]Getting Git version info
2025-12-30T06:02:53.7692290Z Working directory is '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T06:02:53.7694442Z [command]/usr/bin/git version
2025-12-30T06:02:53.7825477Z git version 2.52.0
2025-12-30T06:02:53.7855319Z ##[endgroup]
2025-12-30T06:02:53.7873341Z Temporarily overriding HOME='/home/runner/work/_temp/2a7e2b56-32e5-4526-9d9a-ffa385e77bfb' before making global git config changes
2025-12-30T06:02:53.7878247Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T06:02:53.7882173Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T06:02:53.7923238Z Deleting the contents of '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T06:02:53.7927218Z ##[group]Initializing the repository
2025-12-30T06:02:53.7933185Z [command]/usr/bin/git init /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T06:02:53.8044934Z hint: Using 'master' as the name for the initial branch. This default branch name
2025-12-30T06:02:53.8048013Z hint: will change to "main" in Git 3.0. To configure the initial branch name
2025-12-30T06:02:53.8051295Z hint: to use in all of your new repositories, which will suppress this warning,
2025-12-30T06:02:53.8053689Z hint: call:
2025-12-30T06:02:53.8054801Z hint:
2025-12-30T06:02:53.8056460Z hint: 	git config --global init.defaultBranch <name>
2025-12-30T06:02:53.8058265Z hint:
2025-12-30T06:02:53.8059892Z hint: Names commonly chosen instead of 'master' are 'main', 'trunk' and
2025-12-30T06:02:53.8063076Z hint: 'development'. The just-created branch can be renamed via this command:
2025-12-30T06:02:53.8065451Z hint:
2025-12-30T06:02:53.8066708Z hint: 	git branch -m <name>
2025-12-30T06:02:53.8067976Z hint:
2025-12-30T06:02:53.8069712Z hint: Disable this message with "git config set advice.defaultBranchName false"
2025-12-30T06:02:53.8072361Z Initialized empty Git repository in /home/runner/work/YFinance.NET/YFinance.NET/.git/
2025-12-30T06:02:53.8075623Z [command]/usr/bin/git remote add origin https://github.com/CalvinPangch/YFinance.NET
2025-12-30T06:02:53.8101608Z ##[endgroup]
2025-12-30T06:02:53.8103806Z ##[group]Disabling automatic garbage collection
2025-12-30T06:02:53.8106186Z [command]/usr/bin/git config --local gc.auto 0
2025-12-30T06:02:53.8136538Z ##[endgroup]
2025-12-30T06:02:53.8138599Z ##[group]Setting up auth
2025-12-30T06:02:53.8144705Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T06:02:53.8178323Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T06:02:53.8506530Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T06:02:53.8541462Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T06:02:53.8762774Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T06:02:53.8793701Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T06:02:53.9024006Z [command]/usr/bin/git config --local http.https://github.com/.extraheader AUTHORIZATION: basic ***
2025-12-30T06:02:53.9059319Z ##[endgroup]
2025-12-30T06:02:53.9061540Z ##[group]Fetching the repository
2025-12-30T06:02:53.9071184Z [command]/usr/bin/git -c protocol.version=2 fetch --no-tags --prune --no-recurse-submodules --depth=1 origin +149da10551655e08884fd10692b7cd2d7cc993df:refs/remotes/pull/7/merge
2025-12-30T06:02:54.3568858Z From https://github.com/CalvinPangch/YFinance.NET
2025-12-30T06:02:54.3571428Z  * [new ref]         149da10551655e08884fd10692b7cd2d7cc993df -> pull/7/merge
2025-12-30T06:02:54.3601732Z ##[endgroup]
2025-12-30T06:02:54.3603660Z ##[group]Determining the checkout info
2025-12-30T06:02:54.3605939Z ##[endgroup]
2025-12-30T06:02:54.3608485Z [command]/usr/bin/git sparse-checkout disable
2025-12-30T06:02:54.3649634Z [command]/usr/bin/git config --local --unset-all extensions.worktreeConfig
2025-12-30T06:02:54.3675488Z ##[group]Checking out the ref
2025-12-30T06:02:54.3678089Z [command]/usr/bin/git checkout --progress --force refs/remotes/pull/7/merge
2025-12-30T06:02:54.3799619Z Note: switching to 'refs/remotes/pull/7/merge'.
2025-12-30T06:02:54.3801287Z 
2025-12-30T06:02:54.3802437Z You are in 'detached HEAD' state. You can look around, make experimental
2025-12-30T06:02:54.3804616Z changes and commit them, and you can discard any commits you make in this
2025-12-30T06:02:54.3808140Z state without impacting any branches by switching back to a branch.
2025-12-30T06:02:54.3809983Z 
2025-12-30T06:02:54.3811155Z If you want to create a new branch to retain commits you create, you may
2025-12-30T06:02:54.3813874Z do so (now or later) by using -c with the switch command. Example:
2025-12-30T06:02:54.3815605Z 
2025-12-30T06:02:54.3816489Z   git switch -c <new-branch-name>
2025-12-30T06:02:54.3817836Z 
2025-12-30T06:02:54.3818382Z Or undo this operation with:
2025-12-30T06:02:54.3819276Z 
2025-12-30T06:02:54.3819728Z   git switch -
2025-12-30T06:02:54.3820416Z 
2025-12-30T06:02:54.3821662Z Turn off this advice by setting config variable advice.detachedHead to false
2025-12-30T06:02:54.3822818Z 
2025-12-30T06:02:54.3824049Z HEAD is now at 149da10 Merge d8650052f7ff2fd444b6583803a0c6d3df6031ed into 71fb9da4e8d5107b786fc55175e6a45ef90f93e3
2025-12-30T06:02:54.3829035Z ##[endgroup]
2025-12-30T06:02:54.3847841Z [command]/usr/bin/git log -1 --format=%H
2025-12-30T06:02:54.3870482Z 149da10551655e08884fd10692b7cd2d7cc993df
2025-12-30T06:02:54.4186279Z ##[group]Run actions/setup-dotnet@v4
2025-12-30T06:02:54.4187293Z with:
2025-12-30T06:02:54.4187996Z   dotnet-version: 10.0.x
2025-12-30T06:02:54.4188823Z   cache: false
2025-12-30T06:02:54.4189543Z ##[endgroup]
2025-12-30T06:02:54.5974244Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --runtime dotnet --channel LTS
2025-12-30T06:02:55.1047559Z dotnet-install: .NET Core Runtime with version '10.0.1' is already installed.
2025-12-30T06:02:55.1079936Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --channel 10.0
2025-12-30T06:02:55.4788990Z dotnet-install: .NET Core SDK with version '10.0.101' is already installed.
2025-12-30T06:02:55.4944211Z ##[group]Run dotnet restore
2025-12-30T06:02:55.4944735Z [36;1mdotnet restore[0m
2025-12-30T06:02:55.4985152Z shell: /usr/bin/bash -e {0}
2025-12-30T06:02:55.4985593Z env:
2025-12-30T06:02:55.4986099Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T06:02:55.4986507Z ##[endgroup]
2025-12-30T06:02:59.2414110Z   Determining projects to restore...
2025-12-30T06:03:00.1943781Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/YFinance.NET.Models.csproj (in 169 ms).
2025-12-30T06:03:00.1945443Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj (in 173 ms).
2025-12-30T06:03:02.6447035Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj (in 2.65 sec).
2025-12-30T06:03:02.7142978Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj (in 2.72 sec).
2025-12-30T06:03:02.7573410Z ##[group]Run dotnet build --configuration Release --no-restore /warnaserror
2025-12-30T06:03:02.7573984Z [36;1mdotnet build --configuration Release --no-restore /warnaserror[0m
2025-12-30T06:03:02.7606294Z shell: /usr/bin/bash -e {0}
2025-12-30T06:03:02.7606559Z env:
2025-12-30T06:03:02.7606733Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T06:03:02.7606966Z ##[endgroup]
2025-12-30T06:03:08.7727740Z   YFinance.NET.Models -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/bin/Release/net10.0/YFinance.NET.Models.dll
2025-12-30T06:03:09.2449536Z   YFinance.NET.Interfaces -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/bin/Release/net10.0/YFinance.NET.Interfaces.dll
2025-12-30T06:03:10.6409071Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6422205Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6428447Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6434463Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6440733Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6446125Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6450968Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6456158Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6461047Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6466592Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6543448Z 
2025-12-30T06:03:10.6550257Z Build FAILED.
2025-12-30T06:03:10.6550589Z 
2025-12-30T06:03:10.6553149Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6557260Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6563415Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6570102Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6576823Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6582637Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6586633Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6590480Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6594733Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6598760Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): error CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:10.6600657Z     0 Warning(s)
2025-12-30T06:03:10.6600854Z     10 Error(s)
2025-12-30T06:03:10.6600980Z 
2025-12-30T06:03:10.6601108Z Time Elapsed 00:00:07.61
2025-12-30T06:03:10.6881224Z ##[error]Process completed with exit code 1.
2025-12-30T06:03:10.6991321Z Post job cleanup.
2025-12-30T06:03:10.7940257Z [command]/usr/bin/git version
2025-12-30T06:03:10.7983529Z git version 2.52.0
2025-12-30T06:03:10.8027337Z Temporarily overriding HOME='/home/runner/work/_temp/781cbd6c-023d-41fb-a146-3469163eeb17' before making global git config changes
2025-12-30T06:03:10.8028807Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T06:03:10.8033836Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T06:03:10.8068524Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T06:03:10.8100098Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T06:03:10.8324582Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T06:03:10.8349253Z http.https://github.com/.extraheader
2025-12-30T06:03:10.8361767Z [command]/usr/bin/git config --local --unset-all http.https://github.com/.extraheader
2025-12-30T06:03:10.8401508Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T06:03:10.8750419Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T06:03:10.8810046Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T06:03:10.9348591Z Cleaning up orphan processes
2025-12-30T06:03:10.9668679Z Terminate orphan process: pid (2343) (dotnet)
2025-12-30T06:03:10.9696464Z Terminate orphan process: pid (2344) (dotnet)
2025-12-30T06:03:10.9734880Z Terminate orphan process: pid (2345) (dotnet)
2025-12-30T06:03:10.9758665Z Terminate orphan process: pid (2467) (VBCSCompiler)

```

## Failed Job: Code Coverage

```
2025-12-30T06:02:48.7731824Z Current runner version: '2.330.0'
2025-12-30T06:02:48.7754784Z ##[group]Runner Image Provisioner
2025-12-30T06:02:48.7755753Z Hosted Compute Agent
2025-12-30T06:02:48.7756440Z Version: 20251211.462
2025-12-30T06:02:48.7757314Z Commit: 6cbad8c2bb55d58165063d031ccabf57e2d2db61
2025-12-30T06:02:48.7758318Z Build Date: 2025-12-11T16:28:49Z
2025-12-30T06:02:48.7759083Z Worker ID: {510edacc-6741-4e10-b8e1-6dfe3e4cfb15}
2025-12-30T06:02:48.7759762Z ##[endgroup]
2025-12-30T06:02:48.7760224Z ##[group]Operating System
2025-12-30T06:02:48.7760922Z Ubuntu
2025-12-30T06:02:48.7761354Z 24.04.3
2025-12-30T06:02:48.7761784Z LTS
2025-12-30T06:02:48.7762172Z ##[endgroup]
2025-12-30T06:02:48.7762658Z ##[group]Runner Image
2025-12-30T06:02:48.7763147Z Image: ubuntu-24.04
2025-12-30T06:02:48.7763579Z Version: 20251215.174.1
2025-12-30T06:02:48.7764472Z Included Software: https://github.com/actions/runner-images/blob/ubuntu24/20251215.174/images/ubuntu/Ubuntu2404-Readme.md
2025-12-30T06:02:48.7765827Z Image Release: https://github.com/actions/runner-images/releases/tag/ubuntu24%2F20251215.174
2025-12-30T06:02:48.7766714Z ##[endgroup]
2025-12-30T06:02:48.7767865Z ##[group]GITHUB_TOKEN Permissions
2025-12-30T06:02:48.7769741Z Checks: write
2025-12-30T06:02:48.7770274Z Contents: read
2025-12-30T06:02:48.7770802Z Metadata: read
2025-12-30T06:02:48.7771263Z PullRequests: write
2025-12-30T06:02:48.7771720Z Statuses: write
2025-12-30T06:02:48.7772161Z ##[endgroup]
2025-12-30T06:02:48.7774203Z Secret source: Actions
2025-12-30T06:02:48.7775108Z Prepare workflow directory
2025-12-30T06:02:48.8089057Z Prepare all required actions
2025-12-30T06:02:48.8126122Z Getting action download info
2025-12-30T06:02:49.1215022Z Download action repository 'actions/checkout@v4' (SHA:34e114876b0b11c390a56381ad16ebd13914f8d5)
2025-12-30T06:02:49.2296671Z Download action repository 'actions/setup-dotnet@v4' (SHA:67a3573c9a986a3f9c594539f4ab511d57bb3ce9)
2025-12-30T06:02:49.6424116Z Download action repository 'codecov/codecov-action@v4' (SHA:b9fd7d16f6d7d1b5d2bec1a2887e65ceed900238)
2025-12-30T06:02:50.0628401Z Complete job name: Code Coverage
2025-12-30T06:02:50.1421792Z ##[group]Run actions/checkout@v4
2025-12-30T06:02:50.1422693Z with:
2025-12-30T06:02:50.1423173Z   repository: CalvinPangch/YFinance.NET
2025-12-30T06:02:50.1424040Z   token: ***
2025-12-30T06:02:50.1424468Z   ssh-strict: true
2025-12-30T06:02:50.1424921Z   ssh-user: git
2025-12-30T06:02:50.1425369Z   persist-credentials: true
2025-12-30T06:02:50.1425896Z   clean: true
2025-12-30T06:02:50.1426340Z   sparse-checkout-cone-mode: true
2025-12-30T06:02:50.1426885Z   fetch-depth: 1
2025-12-30T06:02:50.1427308Z   fetch-tags: false
2025-12-30T06:02:50.1427752Z   show-progress: true
2025-12-30T06:02:50.1428199Z   lfs: false
2025-12-30T06:02:50.1428630Z   submodules: false
2025-12-30T06:02:50.1429088Z   set-safe-directory: true
2025-12-30T06:02:50.1429787Z ##[endgroup]
2025-12-30T06:02:50.2441920Z Syncing repository: CalvinPangch/YFinance.NET
2025-12-30T06:02:50.2443937Z ##[group]Getting Git version info
2025-12-30T06:02:50.2444834Z Working directory is '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T06:02:50.2445931Z [command]/usr/bin/git version
2025-12-30T06:02:50.2509573Z git version 2.52.0
2025-12-30T06:02:50.2532448Z ##[endgroup]
2025-12-30T06:02:50.2548130Z Temporarily overriding HOME='/home/runner/work/_temp/a0dd4272-ef82-4567-ba3c-54474c42eff1' before making global git config changes
2025-12-30T06:02:50.2552106Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T06:02:50.2556323Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T06:02:50.2595669Z Deleting the contents of '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T06:02:50.2599233Z ##[group]Initializing the repository
2025-12-30T06:02:50.2603888Z [command]/usr/bin/git init /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T06:02:50.2702583Z hint: Using 'master' as the name for the initial branch. This default branch name
2025-12-30T06:02:50.2704324Z hint: will change to "main" in Git 3.0. To configure the initial branch name
2025-12-30T06:02:50.2705929Z hint: to use in all of your new repositories, which will suppress this warning,
2025-12-30T06:02:50.2707526Z hint: call:
2025-12-30T06:02:50.2708363Z hint:
2025-12-30T06:02:50.2709459Z hint: 	git config --global init.defaultBranch <name>
2025-12-30T06:02:50.2710793Z hint:
2025-12-30T06:02:50.2712000Z hint: Names commonly chosen instead of 'master' are 'main', 'trunk' and
2025-12-30T06:02:50.2713923Z hint: 'development'. The just-created branch can be renamed via this command:
2025-12-30T06:02:50.2715397Z hint:
2025-12-30T06:02:50.2716252Z hint: 	git branch -m <name>
2025-12-30T06:02:50.2717282Z hint:
2025-12-30T06:02:50.2718554Z hint: Disable this message with "git config set advice.defaultBranchName false"
2025-12-30T06:02:50.2720875Z Initialized empty Git repository in /home/runner/work/YFinance.NET/YFinance.NET/.git/
2025-12-30T06:02:50.2724214Z [command]/usr/bin/git remote add origin https://github.com/CalvinPangch/YFinance.NET
2025-12-30T06:02:50.2749635Z ##[endgroup]
2025-12-30T06:02:50.2751373Z ##[group]Disabling automatic garbage collection
2025-12-30T06:02:50.2754313Z [command]/usr/bin/git config --local gc.auto 0
2025-12-30T06:02:50.2778371Z ##[endgroup]
2025-12-30T06:02:50.2779864Z ##[group]Setting up auth
2025-12-30T06:02:50.2785764Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T06:02:50.2811774Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T06:02:50.3124119Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T06:02:50.3149719Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T06:02:50.3328520Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T06:02:50.3353703Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T06:02:50.3525204Z [command]/usr/bin/git config --local http.https://github.com/.extraheader AUTHORIZATION: basic ***
2025-12-30T06:02:50.3554024Z ##[endgroup]
2025-12-30T06:02:50.3555082Z ##[group]Fetching the repository
2025-12-30T06:02:50.3561697Z [command]/usr/bin/git -c protocol.version=2 fetch --no-tags --prune --no-recurse-submodules --depth=1 origin +149da10551655e08884fd10692b7cd2d7cc993df:refs/remotes/pull/7/merge
2025-12-30T06:02:50.6129202Z From https://github.com/CalvinPangch/YFinance.NET
2025-12-30T06:02:50.6130194Z  * [new ref]         149da10551655e08884fd10692b7cd2d7cc993df -> pull/7/merge
2025-12-30T06:02:50.6156971Z ##[endgroup]
2025-12-30T06:02:50.6158540Z ##[group]Determining the checkout info
2025-12-30T06:02:50.6160624Z ##[endgroup]
2025-12-30T06:02:50.6164790Z [command]/usr/bin/git sparse-checkout disable
2025-12-30T06:02:50.6203119Z [command]/usr/bin/git config --local --unset-all extensions.worktreeConfig
2025-12-30T06:02:50.6225415Z ##[group]Checking out the ref
2025-12-30T06:02:50.6229879Z [command]/usr/bin/git checkout --progress --force refs/remotes/pull/7/merge
2025-12-30T06:02:50.6325919Z Note: switching to 'refs/remotes/pull/7/merge'.
2025-12-30T06:02:50.6327143Z 
2025-12-30T06:02:50.6327761Z You are in 'detached HEAD' state. You can look around, make experimental
2025-12-30T06:02:50.6329118Z changes and commit them, and you can discard any commits you make in this
2025-12-30T06:02:50.6329890Z state without impacting any branches by switching back to a branch.
2025-12-30T06:02:50.6330349Z 
2025-12-30T06:02:50.6330785Z If you want to create a new branch to retain commits you create, you may
2025-12-30T06:02:50.6331541Z do so (now or later) by using -c with the switch command. Example:
2025-12-30T06:02:50.6332576Z 
2025-12-30T06:02:50.6332917Z   git switch -c <new-branch-name>
2025-12-30T06:02:50.6333219Z 
2025-12-30T06:02:50.6333396Z Or undo this operation with:
2025-12-30T06:02:50.6333674Z 
2025-12-30T06:02:50.6333834Z   git switch -
2025-12-30T06:02:50.6334037Z 
2025-12-30T06:02:50.6334383Z Turn off this advice by setting config variable advice.detachedHead to false
2025-12-30T06:02:50.6334868Z 
2025-12-30T06:02:50.6335412Z HEAD is now at 149da10 Merge d8650052f7ff2fd444b6583803a0c6d3df6031ed into 71fb9da4e8d5107b786fc55175e6a45ef90f93e3
2025-12-30T06:02:50.6337088Z ##[endgroup]
2025-12-30T06:02:50.6368686Z [command]/usr/bin/git log -1 --format=%H
2025-12-30T06:02:50.6388748Z 149da10551655e08884fd10692b7cd2d7cc993df
2025-12-30T06:02:50.6639182Z ##[group]Run actions/setup-dotnet@v4
2025-12-30T06:02:50.6639684Z with:
2025-12-30T06:02:50.6640026Z   dotnet-version: 10.0.x
2025-12-30T06:02:50.6640543Z   cache: false
2025-12-30T06:02:50.6640882Z ##[endgroup]
2025-12-30T06:02:50.8306292Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --runtime dotnet --channel LTS
2025-12-30T06:02:51.2115297Z dotnet-install: .NET Core Runtime with version '10.0.1' is already installed.
2025-12-30T06:02:51.2161123Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --channel 10.0
2025-12-30T06:02:51.4638397Z dotnet-install: .NET Core SDK with version '10.0.101' is already installed.
2025-12-30T06:02:51.4791143Z ##[group]Run dotnet restore
2025-12-30T06:02:51.4792512Z [36;1mdotnet restore[0m
2025-12-30T06:02:51.4821490Z shell: /usr/bin/bash -e {0}
2025-12-30T06:02:51.4822704Z env:
2025-12-30T06:02:51.4823675Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T06:02:51.4824901Z ##[endgroup]
2025-12-30T06:02:57.7621494Z   Determining projects to restore...
2025-12-30T06:02:59.4969568Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/YFinance.NET.Models.csproj (in 328 ms).
2025-12-30T06:02:59.4973854Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj (in 337 ms).
2025-12-30T06:03:01.4722353Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj (in 2.33 sec).
2025-12-30T06:03:02.1263087Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj (in 2.99 sec).
2025-12-30T06:03:02.1750334Z ##[group]Run dotnet test --configuration Release --collect:"XPlat Code Coverage" --results-directory ./coverage
2025-12-30T06:03:02.1751340Z [36;1mdotnet test --configuration Release --collect:"XPlat Code Coverage" --results-directory ./coverage[0m
2025-12-30T06:03:02.1770614Z shell: /usr/bin/bash -e {0}
2025-12-30T06:03:02.1770878Z env:
2025-12-30T06:03:02.1771061Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T06:03:02.1771287Z ##[endgroup]
2025-12-30T06:03:03.3605782Z   Determining projects to restore...
2025-12-30T06:03:03.8709542Z   All projects are up-to-date for restore.
2025-12-30T06:03:12.2892743Z   YFinance.NET.Models -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/bin/Release/net10.0/YFinance.NET.Models.dll
2025-12-30T06:03:12.6245755Z   YFinance.NET.Interfaces -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/bin/Release/net10.0/YFinance.NET.Interfaces.dll
2025-12-30T06:03:14.0058049Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.0072768Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.0077939Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.0081896Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.0085024Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): error CS0266: Cannot implicitly convert type 'int?' to 'int'. An explicit conversion exists (are you missing a cast?) [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.0088291Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(206,29): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.0090981Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(207,23): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.0093495Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(208,24): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.0095985Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(209,24): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.0098611Z ##[warning]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs(210,30): warning CS8629: Nullable value type may be null. [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj]
2025-12-30T06:03:14.1028995Z ##[error]Process completed with exit code 1.
2025-12-30T06:03:14.1137232Z Post job cleanup.
2025-12-30T06:03:14.2011900Z [command]/usr/bin/git version
2025-12-30T06:03:14.2052089Z git version 2.52.0
2025-12-30T06:03:14.2089204Z Temporarily overriding HOME='/home/runner/work/_temp/4035489d-4f1a-43ea-b262-a8ee4c68de84' before making global git config changes
2025-12-30T06:03:14.2090048Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T06:03:14.2093960Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T06:03:14.2124209Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T06:03:14.2151484Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T06:03:14.2335553Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T06:03:14.2353694Z http.https://github.com/.extraheader
2025-12-30T06:03:14.2364539Z [command]/usr/bin/git config --local --unset-all http.https://github.com/.extraheader
2025-12-30T06:03:14.2389159Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T06:03:14.2560934Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T06:03:14.2587406Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T06:03:14.2855449Z Cleaning up orphan processes
2025-12-30T06:03:14.3075957Z Terminate orphan process: pid (2357) (dotnet)
2025-12-30T06:03:14.3111839Z Terminate orphan process: pid (2358) (dotnet)
2025-12-30T06:03:14.3146771Z Terminate orphan process: pid (2359) (dotnet)
2025-12-30T06:03:14.3282747Z Terminate orphan process: pid (2533) (VBCSCompiler)

```

