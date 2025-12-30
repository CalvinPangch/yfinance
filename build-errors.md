# Build Failure Summary

Branch: claude/fix-security-mjrem4hwy8i13ppr-qFXov
Commit: e3551ca9f3052ddc49ecbb3cc35653a98f35890d
Workflow: CI
Failed Jobs: 3

## Failed Job: Code Coverage

```
2025-12-30T03:54:01.9648168Z Current runner version: '2.330.0'
2025-12-30T03:54:01.9670846Z ##[group]Runner Image Provisioner
2025-12-30T03:54:01.9671630Z Hosted Compute Agent
2025-12-30T03:54:01.9672127Z Version: 20251211.462
2025-12-30T03:54:01.9672639Z Commit: 6cbad8c2bb55d58165063d031ccabf57e2d2db61
2025-12-30T03:54:01.9673299Z Build Date: 2025-12-11T16:28:49Z
2025-12-30T03:54:01.9673909Z Worker ID: {fd9f3946-b9d0-4ff7-bc3e-37a3038a02fb}
2025-12-30T03:54:01.9674498Z ##[endgroup]
2025-12-30T03:54:01.9674991Z ##[group]Operating System
2025-12-30T03:54:01.9675473Z Ubuntu
2025-12-30T03:54:01.9676138Z 24.04.3
2025-12-30T03:54:01.9676550Z LTS
2025-12-30T03:54:01.9676981Z ##[endgroup]
2025-12-30T03:54:01.9677413Z ##[group]Runner Image
2025-12-30T03:54:01.9677897Z Image: ubuntu-24.04
2025-12-30T03:54:01.9678361Z Version: 20251215.174.1
2025-12-30T03:54:01.9679215Z Included Software: https://github.com/actions/runner-images/blob/ubuntu24/20251215.174/images/ubuntu/Ubuntu2404-Readme.md
2025-12-30T03:54:01.9680606Z Image Release: https://github.com/actions/runner-images/releases/tag/ubuntu24%2F20251215.174
2025-12-30T03:54:01.9681432Z ##[endgroup]
2025-12-30T03:54:01.9682587Z ##[group]GITHUB_TOKEN Permissions
2025-12-30T03:54:01.9684624Z Checks: write
2025-12-30T03:54:01.9685058Z Contents: read
2025-12-30T03:54:01.9685581Z Metadata: read
2025-12-30T03:54:01.9686263Z PullRequests: write
2025-12-30T03:54:01.9686702Z Statuses: write
2025-12-30T03:54:01.9687154Z ##[endgroup]
2025-12-30T03:54:01.9689176Z Secret source: Actions
2025-12-30T03:54:01.9689757Z Prepare workflow directory
2025-12-30T03:54:02.0044659Z Prepare all required actions
2025-12-30T03:54:02.0084011Z Getting action download info
2025-12-30T03:54:02.3720780Z Download action repository 'actions/checkout@v4' (SHA:34e114876b0b11c390a56381ad16ebd13914f8d5)
2025-12-30T03:54:02.4368757Z Download action repository 'actions/setup-dotnet@v4' (SHA:67a3573c9a986a3f9c594539f4ab511d57bb3ce9)
2025-12-30T03:54:03.1318182Z Download action repository 'codecov/codecov-action@v4' (SHA:b9fd7d16f6d7d1b5d2bec1a2887e65ceed900238)
2025-12-30T03:54:03.8320518Z Complete job name: Code Coverage
2025-12-30T03:54:03.9091274Z ##[group]Run actions/checkout@v4
2025-12-30T03:54:03.9092129Z with:
2025-12-30T03:54:03.9092565Z   repository: CalvinPangch/YFinance.NET
2025-12-30T03:54:03.9093369Z   token: ***
2025-12-30T03:54:03.9093784Z   ssh-strict: true
2025-12-30T03:54:03.9094192Z   ssh-user: git
2025-12-30T03:54:03.9094622Z   persist-credentials: true
2025-12-30T03:54:03.9095101Z   clean: true
2025-12-30T03:54:03.9095536Z   sparse-checkout-cone-mode: true
2025-12-30T03:54:03.9096358Z   fetch-depth: 1
2025-12-30T03:54:03.9096819Z   fetch-tags: false
2025-12-30T03:54:03.9097240Z   show-progress: true
2025-12-30T03:54:03.9097674Z   lfs: false
2025-12-30T03:54:03.9098080Z   submodules: false
2025-12-30T03:54:03.9098538Z   set-safe-directory: true
2025-12-30T03:54:03.9099312Z ##[endgroup]
2025-12-30T03:54:04.0149061Z Syncing repository: CalvinPangch/YFinance.NET
2025-12-30T03:54:04.0152191Z ##[group]Getting Git version info
2025-12-30T03:54:04.0153996Z Working directory is '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T03:54:04.0156284Z [command]/usr/bin/git version
2025-12-30T03:54:04.0196461Z git version 2.52.0
2025-12-30T03:54:04.0219486Z ##[endgroup]
2025-12-30T03:54:04.0234100Z Temporarily overriding HOME='/home/runner/work/_temp/75f3b660-b093-41f7-bd8d-d8cdebf59c6b' before making global git config changes
2025-12-30T03:54:04.0237883Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T03:54:04.0239959Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T03:54:04.0272351Z Deleting the contents of '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T03:54:04.0276336Z ##[group]Initializing the repository
2025-12-30T03:54:04.0280236Z [command]/usr/bin/git init /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T03:54:04.0362180Z hint: Using 'master' as the name for the initial branch. This default branch name
2025-12-30T03:54:04.0364502Z hint: will change to "main" in Git 3.0. To configure the initial branch name
2025-12-30T03:54:04.0366403Z hint: to use in all of your new repositories, which will suppress this warning,
2025-12-30T03:54:04.0367776Z hint: call:
2025-12-30T03:54:04.0368450Z hint:
2025-12-30T03:54:04.0369421Z hint: 	git config --global init.defaultBranch <name>
2025-12-30T03:54:04.0370501Z hint:
2025-12-30T03:54:04.0371598Z hint: Names commonly chosen instead of 'master' are 'main', 'trunk' and
2025-12-30T03:54:04.0373385Z hint: 'development'. The just-created branch can be renamed via this command:
2025-12-30T03:54:04.0374709Z hint:
2025-12-30T03:54:04.0375384Z hint: 	git branch -m <name>
2025-12-30T03:54:04.0376416Z hint:
2025-12-30T03:54:04.0377562Z hint: Disable this message with "git config set advice.defaultBranchName false"
2025-12-30T03:54:04.0379584Z Initialized empty Git repository in /home/runner/work/YFinance.NET/YFinance.NET/.git/
2025-12-30T03:54:04.0382715Z [command]/usr/bin/git remote add origin https://github.com/CalvinPangch/YFinance.NET
2025-12-30T03:54:04.0404763Z ##[endgroup]
2025-12-30T03:54:04.0406305Z ##[group]Disabling automatic garbage collection
2025-12-30T03:54:04.0409644Z [command]/usr/bin/git config --local gc.auto 0
2025-12-30T03:54:04.0435698Z ##[endgroup]
2025-12-30T03:54:04.0437037Z ##[group]Setting up auth
2025-12-30T03:54:04.0442932Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T03:54:04.0471444Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T03:54:04.0748258Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T03:54:04.0774268Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T03:54:04.0953498Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T03:54:04.0978781Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T03:54:04.1153585Z [command]/usr/bin/git config --local http.https://github.com/.extraheader AUTHORIZATION: basic ***
2025-12-30T03:54:04.1181305Z ##[endgroup]
2025-12-30T03:54:04.1182074Z ##[group]Fetching the repository
2025-12-30T03:54:04.1189172Z [command]/usr/bin/git -c protocol.version=2 fetch --no-tags --prune --no-recurse-submodules --depth=1 origin +ab4c020ba2235b3ec0b37dad8653489cbdc81a8e:refs/remotes/pull/4/merge
2025-12-30T03:54:04.5803769Z From https://github.com/CalvinPangch/YFinance.NET
2025-12-30T03:54:04.5805188Z  * [new ref]         ab4c020ba2235b3ec0b37dad8653489cbdc81a8e -> pull/4/merge
2025-12-30T03:54:04.5828687Z ##[endgroup]
2025-12-30T03:54:04.5829322Z ##[group]Determining the checkout info
2025-12-30T03:54:04.5830515Z ##[endgroup]
2025-12-30T03:54:04.5835169Z [command]/usr/bin/git sparse-checkout disable
2025-12-30T03:54:04.5867530Z [command]/usr/bin/git config --local --unset-all extensions.worktreeConfig
2025-12-30T03:54:04.5886603Z ##[group]Checking out the ref
2025-12-30T03:54:04.5889929Z [command]/usr/bin/git checkout --progress --force refs/remotes/pull/4/merge
2025-12-30T03:54:04.5975182Z Note: switching to 'refs/remotes/pull/4/merge'.
2025-12-30T03:54:04.5975643Z 
2025-12-30T03:54:04.5976203Z You are in 'detached HEAD' state. You can look around, make experimental
2025-12-30T03:54:04.5977454Z changes and commit them, and you can discard any commits you make in this
2025-12-30T03:54:04.5978761Z state without impacting any branches by switching back to a branch.
2025-12-30T03:54:04.5979465Z 
2025-12-30T03:54:04.5979988Z If you want to create a new branch to retain commits you create, you may
2025-12-30T03:54:04.5981108Z do so (now or later) by using -c with the switch command. Example:
2025-12-30T03:54:04.5982136Z 
2025-12-30T03:54:04.5982522Z   git switch -c <new-branch-name>
2025-12-30T03:54:04.5983021Z 
2025-12-30T03:54:04.5983321Z Or undo this operation with:
2025-12-30T03:54:04.5983762Z 
2025-12-30T03:54:04.5984003Z   git switch -
2025-12-30T03:54:04.5984372Z 
2025-12-30T03:54:04.5984955Z Turn off this advice by setting config variable advice.detachedHead to false
2025-12-30T03:54:04.5985783Z 
2025-12-30T03:54:04.5986840Z HEAD is now at ab4c020 Merge e3551ca9f3052ddc49ecbb3cc35653a98f35890d into 06f9addc01c7a2bb4c339c037ebce9b43bf208cf
2025-12-30T03:54:04.5989640Z ##[endgroup]
2025-12-30T03:54:04.6015975Z [command]/usr/bin/git log -1 --format=%H
2025-12-30T03:54:04.6033870Z ab4c020ba2235b3ec0b37dad8653489cbdc81a8e
2025-12-30T03:54:04.6227651Z ##[group]Run actions/setup-dotnet@v4
2025-12-30T03:54:04.6228138Z with:
2025-12-30T03:54:04.6228485Z   dotnet-version: 10.0.x
2025-12-30T03:54:04.6228862Z   cache: false
2025-12-30T03:54:04.6229203Z ##[endgroup]
2025-12-30T03:54:04.7886611Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --runtime dotnet --channel LTS
2025-12-30T03:54:05.1601966Z dotnet-install: .NET Core Runtime with version '10.0.1' is already installed.
2025-12-30T03:54:05.1631110Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --channel 10.0
2025-12-30T03:54:05.4847468Z dotnet-install: .NET Core SDK with version '10.0.101' is already installed.
2025-12-30T03:54:05.5016081Z ##[group]Run dotnet restore
2025-12-30T03:54:05.5017220Z [36;1mdotnet restore[0m
2025-12-30T03:54:05.5044953Z shell: /usr/bin/bash -e {0}
2025-12-30T03:54:05.5046210Z env:
2025-12-30T03:54:05.5046960Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T03:54:05.5047913Z ##[endgroup]
2025-12-30T03:54:11.0679310Z   Determining projects to restore...
2025-12-30T03:54:12.0993057Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/YFinance.NET.Models.csproj (in 189 ms).
2025-12-30T03:54:12.0994766Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj (in 189 ms).
2025-12-30T03:54:14.3116030Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj (in 2.44 sec).
2025-12-30T03:54:14.4206619Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj (in 2.55 sec).
2025-12-30T03:54:14.4524077Z ##[group]Run dotnet test --configuration Release --collect:"XPlat Code Coverage" --results-directory ./coverage
2025-12-30T03:54:14.4524822Z [36;1mdotnet test --configuration Release --collect:"XPlat Code Coverage" --results-directory ./coverage[0m
2025-12-30T03:54:14.4543916Z shell: /usr/bin/bash -e {0}
2025-12-30T03:54:14.4544153Z env:
2025-12-30T03:54:14.4544329Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T03:54:14.4544540Z ##[endgroup]
2025-12-30T03:54:15.6586356Z   Determining projects to restore...
2025-12-30T03:54:16.1502872Z   All projects are up-to-date for restore.
2025-12-30T03:54:21.9102030Z   YFinance.NET.Models -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/bin/Release/net10.0/YFinance.NET.Models.dll
2025-12-30T03:54:22.2532271Z   YFinance.NET.Interfaces -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/bin/Release/net10.0/YFinance.NET.Interfaces.dll
2025-12-30T03:54:23.8213758Z   YFinance.NET.Implementation -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/bin/Release/net10.0/YFinance.NET.Implementation.dll
2025-12-30T03:54:25.3274066Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/SharesScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'SharesScraper.SharesScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3297491Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3304860Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(35,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3311859Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(43,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3318329Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/AnalysisScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'AnalysisScraper.AnalysisScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3325074Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/CalendarScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'CalendarScraper.CalendarScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3332725Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/EsgScraperTests.cs(20,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'EsgScraper.EsgScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3339034Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/FundamentalsScraperTests.cs(47,27): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'FundamentalsScraper.FundamentalsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3343825Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(37,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3348183Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HoldersScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HoldersScraper.HoldersScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3352780Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(246,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3357544Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(282,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3361507Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/OptionsScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'OptionsScraper.OptionsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3365515Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(580,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3371091Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(588,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:25.3982135Z ##[error]Process completed with exit code 1.
2025-12-30T03:54:25.4092400Z Post job cleanup.
2025-12-30T03:54:25.4973495Z [command]/usr/bin/git version
2025-12-30T03:54:25.5014431Z git version 2.52.0
2025-12-30T03:54:25.5053332Z Temporarily overriding HOME='/home/runner/work/_temp/f67553fe-1285-4729-8254-458007e33ea0' before making global git config changes
2025-12-30T03:54:25.5054583Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T03:54:25.5058083Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T03:54:25.5088002Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T03:54:25.5115463Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T03:54:25.5293079Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T03:54:25.5312025Z http.https://github.com/.extraheader
2025-12-30T03:54:25.5324020Z [command]/usr/bin/git config --local --unset-all http.https://github.com/.extraheader
2025-12-30T03:54:25.5350644Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T03:54:25.5520870Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T03:54:25.5549703Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T03:54:25.5808689Z Cleaning up orphan processes
2025-12-30T03:54:25.6040478Z Terminate orphan process: pid (2357) (dotnet)
2025-12-30T03:54:25.6075769Z Terminate orphan process: pid (2358) (dotnet)
2025-12-30T03:54:25.6109341Z Terminate orphan process: pid (2359) (dotnet)
2025-12-30T03:54:25.6230158Z Terminate orphan process: pid (2526) (VBCSCompiler)

```

## Failed Job: Code Quality Check

```
2025-12-30T03:54:02.0613619Z Current runner version: '2.330.0'
2025-12-30T03:54:02.0636197Z ##[group]Runner Image Provisioner
2025-12-30T03:54:02.0636994Z Hosted Compute Agent
2025-12-30T03:54:02.0637825Z Version: 20251211.462
2025-12-30T03:54:02.0638435Z Commit: 6cbad8c2bb55d58165063d031ccabf57e2d2db61
2025-12-30T03:54:02.0639074Z Build Date: 2025-12-11T16:28:49Z
2025-12-30T03:54:02.0639786Z Worker ID: {0711e653-925b-4e12-9c5d-6aa62bc518e4}
2025-12-30T03:54:02.0640477Z ##[endgroup]
2025-12-30T03:54:02.0640958Z ##[group]Operating System
2025-12-30T03:54:02.0641568Z Ubuntu
2025-12-30T03:54:02.0642006Z 24.04.3
2025-12-30T03:54:02.0642449Z LTS
2025-12-30T03:54:02.0642888Z ##[endgroup]
2025-12-30T03:54:02.0643415Z ##[group]Runner Image
2025-12-30T03:54:02.0643894Z Image: ubuntu-24.04
2025-12-30T03:54:02.0644430Z Version: 20251215.174.1
2025-12-30T03:54:02.0645439Z Included Software: https://github.com/actions/runner-images/blob/ubuntu24/20251215.174/images/ubuntu/Ubuntu2404-Readme.md
2025-12-30T03:54:02.0646886Z Image Release: https://github.com/actions/runner-images/releases/tag/ubuntu24%2F20251215.174
2025-12-30T03:54:02.0648083Z ##[endgroup]
2025-12-30T03:54:02.0649382Z ##[group]GITHUB_TOKEN Permissions
2025-12-30T03:54:02.0651222Z Checks: write
2025-12-30T03:54:02.0651756Z Contents: read
2025-12-30T03:54:02.0652194Z Metadata: read
2025-12-30T03:54:02.0652833Z PullRequests: write
2025-12-30T03:54:02.0653302Z Statuses: write
2025-12-30T03:54:02.0653770Z ##[endgroup]
2025-12-30T03:54:02.0655712Z Secret source: Actions
2025-12-30T03:54:02.0656373Z Prepare workflow directory
2025-12-30T03:54:02.0986506Z Prepare all required actions
2025-12-30T03:54:02.1024563Z Getting action download info
2025-12-30T03:54:02.4615727Z Download action repository 'actions/checkout@v4' (SHA:34e114876b0b11c390a56381ad16ebd13914f8d5)
2025-12-30T03:54:02.5677219Z Download action repository 'actions/setup-dotnet@v4' (SHA:67a3573c9a986a3f9c594539f4ab511d57bb3ce9)
2025-12-30T03:54:03.3135760Z Complete job name: Code Quality Check
2025-12-30T03:54:03.3804311Z ##[group]Run actions/checkout@v4
2025-12-30T03:54:03.3805135Z with:
2025-12-30T03:54:03.3805540Z   repository: CalvinPangch/YFinance.NET
2025-12-30T03:54:03.3806195Z   token: ***
2025-12-30T03:54:03.3806552Z   ssh-strict: true
2025-12-30T03:54:03.3806932Z   ssh-user: git
2025-12-30T03:54:03.3807314Z   persist-credentials: true
2025-12-30T03:54:03.3807896Z   clean: true
2025-12-30T03:54:03.3808282Z   sparse-checkout-cone-mode: true
2025-12-30T03:54:03.3808753Z   fetch-depth: 1
2025-12-30T03:54:03.3809119Z   fetch-tags: false
2025-12-30T03:54:03.3809516Z   show-progress: true
2025-12-30T03:54:03.3809904Z   lfs: false
2025-12-30T03:54:03.3810267Z   submodules: false
2025-12-30T03:54:03.3810665Z   set-safe-directory: true
2025-12-30T03:54:03.3811307Z ##[endgroup]
2025-12-30T03:54:03.4891751Z Syncing repository: CalvinPangch/YFinance.NET
2025-12-30T03:54:03.4893603Z ##[group]Getting Git version info
2025-12-30T03:54:03.4894421Z Working directory is '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T03:54:03.4895396Z [command]/usr/bin/git version
2025-12-30T03:54:03.4992528Z git version 2.52.0
2025-12-30T03:54:03.5017824Z ##[endgroup]
2025-12-30T03:54:03.5031438Z Temporarily overriding HOME='/home/runner/work/_temp/4bddf795-40fe-4616-ae01-deca21754bd2' before making global git config changes
2025-12-30T03:54:03.5033593Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T03:54:03.5036229Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T03:54:03.5071610Z Deleting the contents of '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T03:54:03.5075073Z ##[group]Initializing the repository
2025-12-30T03:54:03.5079130Z [command]/usr/bin/git init /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T03:54:03.5166459Z hint: Using 'master' as the name for the initial branch. This default branch name
2025-12-30T03:54:03.5168333Z hint: will change to "main" in Git 3.0. To configure the initial branch name
2025-12-30T03:54:03.5169791Z hint: to use in all of your new repositories, which will suppress this warning,
2025-12-30T03:54:03.5170907Z hint: call:
2025-12-30T03:54:03.5171465Z hint:
2025-12-30T03:54:03.5172326Z hint: 	git config --global init.defaultBranch <name>
2025-12-30T03:54:03.5173226Z hint:
2025-12-30T03:54:03.5173984Z hint: Names commonly chosen instead of 'master' are 'main', 'trunk' and
2025-12-30T03:54:03.5174847Z hint: 'development'. The just-created branch can be renamed via this command:
2025-12-30T03:54:03.5175685Z hint:
2025-12-30T03:54:03.5176278Z hint: 	git branch -m <name>
2025-12-30T03:54:03.5176716Z hint:
2025-12-30T03:54:03.5177286Z hint: Disable this message with "git config set advice.defaultBranchName false"
2025-12-30T03:54:03.5178535Z Initialized empty Git repository in /home/runner/work/YFinance.NET/YFinance.NET/.git/
2025-12-30T03:54:03.5182036Z [command]/usr/bin/git remote add origin https://github.com/CalvinPangch/YFinance.NET
2025-12-30T03:54:03.5214248Z ##[endgroup]
2025-12-30T03:54:03.5214970Z ##[group]Disabling automatic garbage collection
2025-12-30T03:54:03.5218240Z [command]/usr/bin/git config --local gc.auto 0
2025-12-30T03:54:03.5245523Z ##[endgroup]
2025-12-30T03:54:03.5246187Z ##[group]Setting up auth
2025-12-30T03:54:03.5252296Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T03:54:03.5281092Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T03:54:03.5589326Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T03:54:03.5616472Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T03:54:03.5834955Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T03:54:03.5866387Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T03:54:03.6085026Z [command]/usr/bin/git config --local http.https://github.com/.extraheader AUTHORIZATION: basic ***
2025-12-30T03:54:03.6119719Z ##[endgroup]
2025-12-30T03:54:03.6120943Z ##[group]Fetching the repository
2025-12-30T03:54:03.6129341Z [command]/usr/bin/git -c protocol.version=2 fetch --no-tags --prune --no-recurse-submodules --depth=1 origin +ab4c020ba2235b3ec0b37dad8653489cbdc81a8e:refs/remotes/pull/4/merge
2025-12-30T03:54:04.0954055Z From https://github.com/CalvinPangch/YFinance.NET
2025-12-30T03:54:04.0956384Z  * [new ref]         ab4c020ba2235b3ec0b37dad8653489cbdc81a8e -> pull/4/merge
2025-12-30T03:54:04.0989617Z ##[endgroup]
2025-12-30T03:54:04.0991649Z ##[group]Determining the checkout info
2025-12-30T03:54:04.0993922Z ##[endgroup]
2025-12-30T03:54:04.0997760Z [command]/usr/bin/git sparse-checkout disable
2025-12-30T03:54:04.1040714Z [command]/usr/bin/git config --local --unset-all extensions.worktreeConfig
2025-12-30T03:54:04.1068907Z ##[group]Checking out the ref
2025-12-30T03:54:04.1072749Z [command]/usr/bin/git checkout --progress --force refs/remotes/pull/4/merge
2025-12-30T03:54:04.1188921Z Note: switching to 'refs/remotes/pull/4/merge'.
2025-12-30T03:54:04.1192382Z 
2025-12-30T03:54:04.1193635Z You are in 'detached HEAD' state. You can look around, make experimental
2025-12-30T03:54:04.1196625Z changes and commit them, and you can discard any commits you make in this
2025-12-30T03:54:04.1199963Z state without impacting any branches by switching back to a branch.
2025-12-30T03:54:04.1201770Z 
2025-12-30T03:54:04.1202925Z If you want to create a new branch to retain commits you create, you may
2025-12-30T03:54:04.1205629Z do so (now or later) by using -c with the switch command. Example:
2025-12-30T03:54:04.1207221Z 
2025-12-30T03:54:04.1208057Z   git switch -c <new-branch-name>
2025-12-30T03:54:04.1209527Z 
2025-12-30T03:54:04.1210118Z Or undo this operation with:
2025-12-30T03:54:04.1211168Z 
2025-12-30T03:54:04.1211675Z   git switch -
2025-12-30T03:54:04.1212466Z 
2025-12-30T03:54:04.1213877Z Turn off this advice by setting config variable advice.detachedHead to false
2025-12-30T03:54:04.1216169Z 
2025-12-30T03:54:04.1218983Z HEAD is now at ab4c020 Merge e3551ca9f3052ddc49ecbb3cc35653a98f35890d into 06f9addc01c7a2bb4c339c037ebce9b43bf208cf
2025-12-30T03:54:04.1226265Z ##[endgroup]
2025-12-30T03:54:04.1235150Z [command]/usr/bin/git log -1 --format=%H
2025-12-30T03:54:04.1257188Z ab4c020ba2235b3ec0b37dad8653489cbdc81a8e
2025-12-30T03:54:04.1578651Z ##[group]Run actions/setup-dotnet@v4
2025-12-30T03:54:04.1579859Z with:
2025-12-30T03:54:04.1580666Z   dotnet-version: 10.0.x
2025-12-30T03:54:04.1581637Z   cache: false
2025-12-30T03:54:04.1582466Z ##[endgroup]
2025-12-30T03:54:04.3386545Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --runtime dotnet --channel LTS
2025-12-30T03:54:04.7794453Z dotnet-install: .NET Core Runtime with version '10.0.1' is already installed.
2025-12-30T03:54:04.7841022Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --channel 10.0
2025-12-30T03:54:05.1536072Z dotnet-install: .NET Core SDK with version '10.0.101' is already installed.
2025-12-30T03:54:05.1673425Z ##[group]Run dotnet restore
2025-12-30T03:54:05.1673810Z [36;1mdotnet restore[0m
2025-12-30T03:54:05.1711941Z shell: /usr/bin/bash -e {0}
2025-12-30T03:54:05.1712364Z env:
2025-12-30T03:54:05.1712713Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T03:54:05.1712985Z ##[endgroup]
2025-12-30T03:54:10.1437026Z   Determining projects to restore...
2025-12-30T03:54:11.2556758Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj (in 208 ms).
2025-12-30T03:54:11.2558481Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/YFinance.NET.Models.csproj (in 207 ms).
2025-12-30T03:54:13.2744366Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj (in 2.27 sec).
2025-12-30T03:54:13.5516243Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj (in 2.54 sec).
2025-12-30T03:54:13.5989229Z ##[group]Run dotnet build --configuration Release --no-restore /warnaserror
2025-12-30T03:54:13.5989820Z [36;1mdotnet build --configuration Release --no-restore /warnaserror[0m
2025-12-30T03:54:13.6025779Z shell: /usr/bin/bash -e {0}
2025-12-30T03:54:13.6026255Z env:
2025-12-30T03:54:13.6026462Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T03:54:13.6026694Z ##[endgroup]
2025-12-30T03:54:20.6690177Z   YFinance.NET.Models -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/bin/Release/net10.0/YFinance.NET.Models.dll
2025-12-30T03:54:21.0998883Z   YFinance.NET.Interfaces -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/bin/Release/net10.0/YFinance.NET.Interfaces.dll
2025-12-30T03:54:22.6764591Z   YFinance.NET.Implementation -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/bin/Release/net10.0/YFinance.NET.Implementation.dll
2025-12-30T03:54:24.1557431Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/SharesScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'SharesScraper.SharesScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1594146Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/AnalysisScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'AnalysisScraper.AnalysisScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1602570Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/CalendarScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'CalendarScraper.CalendarScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1623817Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/EsgScraperTests.cs(20,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'EsgScraper.EsgScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1642477Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HoldersScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HoldersScraper.HoldersScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1650365Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(37,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1658718Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/FundamentalsScraperTests.cs(47,27): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'FundamentalsScraper.FundamentalsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1666938Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(246,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1675691Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(282,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1683953Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1692009Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/OptionsScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'OptionsScraper.OptionsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1700002Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(35,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1708547Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(580,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1716591Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(43,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1721693Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(588,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1733944Z 
2025-12-30T03:54:24.1740732Z Build FAILED.
2025-12-30T03:54:24.1742980Z 
2025-12-30T03:54:24.1752165Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/SharesScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'SharesScraper.SharesScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1758218Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/AnalysisScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'AnalysisScraper.AnalysisScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1763359Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/CalendarScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'CalendarScraper.CalendarScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1771515Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/EsgScraperTests.cs(20,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'EsgScraper.EsgScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1778681Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HoldersScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HoldersScraper.HoldersScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1787230Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(37,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1793678Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/FundamentalsScraperTests.cs(47,27): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'FundamentalsScraper.FundamentalsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1798585Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(246,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1803363Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(282,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1807998Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1812434Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/OptionsScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'OptionsScraper.OptionsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1816651Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(35,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1821408Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(580,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1825601Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(43,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1830579Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(588,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:24.1832887Z     0 Warning(s)
2025-12-30T03:54:24.1833074Z     15 Error(s)
2025-12-30T03:54:24.1833196Z 
2025-12-30T03:54:24.1833314Z Time Elapsed 00:00:10.25
2025-12-30T03:54:24.2099139Z ##[error]Process completed with exit code 1.
2025-12-30T03:54:24.2210738Z Post job cleanup.
2025-12-30T03:54:24.3143943Z [command]/usr/bin/git version
2025-12-30T03:54:24.3186367Z git version 2.52.0
2025-12-30T03:54:24.3229054Z Temporarily overriding HOME='/home/runner/work/_temp/545fd1dd-1241-4d56-b861-08fb51466bab' before making global git config changes
2025-12-30T03:54:24.3230090Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T03:54:24.3234324Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T03:54:24.3268464Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T03:54:24.3301182Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T03:54:24.3527938Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T03:54:24.3563643Z http.https://github.com/.extraheader
2025-12-30T03:54:24.3564782Z [command]/usr/bin/git config --local --unset-all http.https://github.com/.extraheader
2025-12-30T03:54:24.3597798Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T03:54:24.3874416Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T03:54:24.3935053Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T03:54:24.4381375Z Cleaning up orphan processes
2025-12-30T03:54:24.4674000Z Terminate orphan process: pid (2331) (dotnet)
2025-12-30T03:54:24.4694141Z Terminate orphan process: pid (2332) (dotnet)
2025-12-30T03:54:24.4722831Z Terminate orphan process: pid (2333) (dotnet)
2025-12-30T03:54:24.4775813Z Terminate orphan process: pid (2454) (VBCSCompiler)

```

## Failed Job: Build and Test

```
2025-12-30T03:54:01.7129847Z Current runner version: '2.330.0'
2025-12-30T03:54:01.7153206Z ##[group]Runner Image Provisioner
2025-12-30T03:54:01.7154024Z Hosted Compute Agent
2025-12-30T03:54:01.7154621Z Version: 20251211.462
2025-12-30T03:54:01.7155174Z Commit: 6cbad8c2bb55d58165063d031ccabf57e2d2db61
2025-12-30T03:54:01.7155909Z Build Date: 2025-12-11T16:28:49Z
2025-12-30T03:54:01.7156529Z Worker ID: {c5802228-9754-402c-af4c-30eac02f32e0}
2025-12-30T03:54:01.7157179Z ##[endgroup]
2025-12-30T03:54:01.7157920Z ##[group]Operating System
2025-12-30T03:54:01.7158477Z Ubuntu
2025-12-30T03:54:01.7158935Z 24.04.3
2025-12-30T03:54:01.7159326Z LTS
2025-12-30T03:54:01.7159836Z ##[endgroup]
2025-12-30T03:54:01.7160316Z ##[group]Runner Image
2025-12-30T03:54:01.7160871Z Image: ubuntu-24.04
2025-12-30T03:54:01.7161401Z Version: 20251215.174.1
2025-12-30T03:54:01.7162390Z Included Software: https://github.com/actions/runner-images/blob/ubuntu24/20251215.174/images/ubuntu/Ubuntu2404-Readme.md
2025-12-30T03:54:01.7163915Z Image Release: https://github.com/actions/runner-images/releases/tag/ubuntu24%2F20251215.174
2025-12-30T03:54:01.7164890Z ##[endgroup]
2025-12-30T03:54:01.7166265Z ##[group]GITHUB_TOKEN Permissions
2025-12-30T03:54:01.7168799Z Checks: write
2025-12-30T03:54:01.7169303Z Contents: read
2025-12-30T03:54:01.7169885Z Metadata: read
2025-12-30T03:54:01.7170348Z PullRequests: write
2025-12-30T03:54:01.7170988Z Statuses: write
2025-12-30T03:54:01.7171548Z ##[endgroup]
2025-12-30T03:54:01.7173639Z Secret source: Actions
2025-12-30T03:54:01.7174623Z Prepare workflow directory
2025-12-30T03:54:01.7494324Z Prepare all required actions
2025-12-30T03:54:01.7531186Z Getting action download info
2025-12-30T03:54:02.1246384Z Download action repository 'actions/checkout@v4' (SHA:34e114876b0b11c390a56381ad16ebd13914f8d5)
2025-12-30T03:54:02.4650735Z Download action repository 'actions/setup-dotnet@v4' (SHA:67a3573c9a986a3f9c594539f4ab511d57bb3ce9)
2025-12-30T03:54:02.9855253Z Download action repository 'actions/upload-artifact@v4' (SHA:ea165f8d65b6e75b540449e92b4886f43607fa02)
2025-12-30T03:54:03.1025457Z Download action repository 'dorny/test-reporter@v1' (SHA:d61b558e8df85cb60d09ca3e5b09653b4477cea7)
2025-12-30T03:54:03.7354260Z Complete job name: Build and Test
2025-12-30T03:54:03.8182236Z ##[group]Run actions/checkout@v4
2025-12-30T03:54:03.8183540Z with:
2025-12-30T03:54:03.8184339Z   repository: CalvinPangch/YFinance.NET
2025-12-30T03:54:03.8185707Z   token: ***
2025-12-30T03:54:03.8186450Z   ssh-strict: true
2025-12-30T03:54:03.8187255Z   ssh-user: git
2025-12-30T03:54:03.8188220Z   persist-credentials: true
2025-12-30T03:54:03.8189139Z   clean: true
2025-12-30T03:54:03.8189944Z   sparse-checkout-cone-mode: true
2025-12-30T03:54:03.8190950Z   fetch-depth: 1
2025-12-30T03:54:03.8191739Z   fetch-tags: false
2025-12-30T03:54:03.8192554Z   show-progress: true
2025-12-30T03:54:03.8193375Z   lfs: false
2025-12-30T03:54:03.8194126Z   submodules: false
2025-12-30T03:54:03.8194954Z   set-safe-directory: true
2025-12-30T03:54:03.8196118Z ##[endgroup]
2025-12-30T03:54:03.9281173Z Syncing repository: CalvinPangch/YFinance.NET
2025-12-30T03:54:03.9283823Z ##[group]Getting Git version info
2025-12-30T03:54:03.9285361Z Working directory is '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T03:54:03.9287486Z [command]/usr/bin/git version
2025-12-30T03:54:03.9358486Z git version 2.52.0
2025-12-30T03:54:03.9384925Z ##[endgroup]
2025-12-30T03:54:03.9400338Z Temporarily overriding HOME='/home/runner/work/_temp/092bebff-6ea7-4de4-bc29-ff63ce370f4d' before making global git config changes
2025-12-30T03:54:03.9404573Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T03:54:03.9407023Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T03:54:03.9445563Z Deleting the contents of '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T03:54:03.9449171Z ##[group]Initializing the repository
2025-12-30T03:54:03.9453042Z [command]/usr/bin/git init /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T03:54:03.9582919Z hint: Using 'master' as the name for the initial branch. This default branch name
2025-12-30T03:54:03.9584927Z hint: will change to "main" in Git 3.0. To configure the initial branch name
2025-12-30T03:54:03.9586753Z hint: to use in all of your new repositories, which will suppress this warning,
2025-12-30T03:54:03.9588635Z hint: call:
2025-12-30T03:54:03.9589373Z hint:
2025-12-30T03:54:03.9590338Z hint: 	git config --global init.defaultBranch <name>
2025-12-30T03:54:03.9592030Z hint:
2025-12-30T03:54:03.9593117Z hint: Names commonly chosen instead of 'master' are 'main', 'trunk' and
2025-12-30T03:54:03.9594901Z hint: 'development'. The just-created branch can be renamed via this command:
2025-12-30T03:54:03.9596504Z hint:
2025-12-30T03:54:03.9597264Z hint: 	git branch -m <name>
2025-12-30T03:54:03.9598370Z hint:
2025-12-30T03:54:03.9599543Z hint: Disable this message with "git config set advice.defaultBranchName false"
2025-12-30T03:54:03.9602015Z Initialized empty Git repository in /home/runner/work/YFinance.NET/YFinance.NET/.git/
2025-12-30T03:54:03.9607487Z [command]/usr/bin/git remote add origin https://github.com/CalvinPangch/YFinance.NET
2025-12-30T03:54:03.9639420Z ##[endgroup]
2025-12-30T03:54:03.9641910Z ##[group]Disabling automatic garbage collection
2025-12-30T03:54:03.9644215Z [command]/usr/bin/git config --local gc.auto 0
2025-12-30T03:54:03.9673771Z ##[endgroup]
2025-12-30T03:54:03.9676058Z ##[group]Setting up auth
2025-12-30T03:54:03.9681419Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T03:54:03.9714397Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T03:54:04.0122253Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T03:54:04.0154126Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T03:54:04.0376990Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T03:54:04.0408714Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T03:54:04.0629991Z [command]/usr/bin/git config --local http.https://github.com/.extraheader AUTHORIZATION: basic ***
2025-12-30T03:54:04.0663374Z ##[endgroup]
2025-12-30T03:54:04.0665781Z ##[group]Fetching the repository
2025-12-30T03:54:04.0674997Z [command]/usr/bin/git -c protocol.version=2 fetch --no-tags --prune --no-recurse-submodules --depth=1 origin +ab4c020ba2235b3ec0b37dad8653489cbdc81a8e:refs/remotes/pull/4/merge
2025-12-30T03:54:04.4132317Z From https://github.com/CalvinPangch/YFinance.NET
2025-12-30T03:54:04.4135576Z  * [new ref]         ab4c020ba2235b3ec0b37dad8653489cbdc81a8e -> pull/4/merge
2025-12-30T03:54:04.4165044Z ##[endgroup]
2025-12-30T03:54:04.4166798Z ##[group]Determining the checkout info
2025-12-30T03:54:04.4168533Z ##[endgroup]
2025-12-30T03:54:04.4172090Z [command]/usr/bin/git sparse-checkout disable
2025-12-30T03:54:04.4211890Z [command]/usr/bin/git config --local --unset-all extensions.worktreeConfig
2025-12-30T03:54:04.4236329Z ##[group]Checking out the ref
2025-12-30T03:54:04.4239751Z [command]/usr/bin/git checkout --progress --force refs/remotes/pull/4/merge
2025-12-30T03:54:04.4361751Z Note: switching to 'refs/remotes/pull/4/merge'.
2025-12-30T03:54:04.4363212Z 
2025-12-30T03:54:04.4364359Z You are in 'detached HEAD' state. You can look around, make experimental
2025-12-30T03:54:04.4367007Z changes and commit them, and you can discard any commits you make in this
2025-12-30T03:54:04.4369070Z state without impacting any branches by switching back to a branch.
2025-12-30T03:54:04.4370283Z 
2025-12-30T03:54:04.4371407Z If you want to create a new branch to retain commits you create, you may
2025-12-30T03:54:04.4373502Z do so (now or later) by using -c with the switch command. Example:
2025-12-30T03:54:04.4374588Z 
2025-12-30T03:54:04.4375012Z   git switch -c <new-branch-name>
2025-12-30T03:54:04.4375651Z 
2025-12-30T03:54:04.4376042Z Or undo this operation with:
2025-12-30T03:54:04.4376617Z 
2025-12-30T03:54:04.4376953Z   git switch -
2025-12-30T03:54:04.4377418Z 
2025-12-30T03:54:04.4378639Z Turn off this advice by setting config variable advice.detachedHead to false
2025-12-30T03:54:04.4379760Z 
2025-12-30T03:54:04.4381003Z HEAD is now at ab4c020 Merge e3551ca9f3052ddc49ecbb3cc35653a98f35890d into 06f9addc01c7a2bb4c339c037ebce9b43bf208cf
2025-12-30T03:54:04.4384439Z ##[endgroup]
2025-12-30T03:54:04.4405919Z [command]/usr/bin/git log -1 --format=%H
2025-12-30T03:54:04.4428410Z ab4c020ba2235b3ec0b37dad8653489cbdc81a8e
2025-12-30T03:54:04.4691022Z ##[group]Run actions/setup-dotnet@v4
2025-12-30T03:54:04.4691995Z with:
2025-12-30T03:54:04.4692677Z   dotnet-version: 10.0.x
2025-12-30T03:54:04.4693522Z   cache: false
2025-12-30T03:54:04.4761651Z ##[endgroup]
2025-12-30T03:54:04.6522304Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --runtime dotnet --channel LTS
2025-12-30T03:54:05.5911003Z dotnet-install: .NET Core Runtime with version '10.0.1' is already installed.
2025-12-30T03:54:05.5958219Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --channel 10.0
2025-12-30T03:54:06.3495800Z dotnet-install: .NET Core SDK with version '10.0.101' is already installed.
2025-12-30T03:54:06.3637288Z ##[group]Run dotnet restore
2025-12-30T03:54:06.3637756Z [36;1mdotnet restore[0m
2025-12-30T03:54:06.3675663Z shell: /usr/bin/bash -e {0}
2025-12-30T03:54:06.3675922Z env:
2025-12-30T03:54:06.3676102Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T03:54:06.3676337Z ##[endgroup]
2025-12-30T03:54:15.1543032Z   Determining projects to restore...
2025-12-30T03:54:17.0594342Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj (in 294 ms).
2025-12-30T03:54:17.0608384Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/YFinance.NET.Models.csproj (in 265 ms).
2025-12-30T03:54:19.5390985Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj (in 2.86 sec).
2025-12-30T03:54:19.8245240Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj (in 3.18 sec).
2025-12-30T03:54:19.8645646Z ##[group]Run dotnet build --configuration Release --no-restore
2025-12-30T03:54:19.8646136Z [36;1mdotnet build --configuration Release --no-restore[0m
2025-12-30T03:54:19.8680292Z shell: /usr/bin/bash -e {0}
2025-12-30T03:54:19.8680564Z env:
2025-12-30T03:54:19.8680762Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T03:54:19.8681141Z ##[endgroup]
2025-12-30T03:54:27.5655330Z   YFinance.NET.Models -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/bin/Release/net10.0/YFinance.NET.Models.dll
2025-12-30T03:54:28.0348749Z   YFinance.NET.Interfaces -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/bin/Release/net10.0/YFinance.NET.Interfaces.dll
2025-12-30T03:54:29.4020543Z   YFinance.NET.Implementation -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/bin/Release/net10.0/YFinance.NET.Implementation.dll
2025-12-30T03:54:30.7791215Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/SharesScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'SharesScraper.SharesScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7826059Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/AnalysisScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'AnalysisScraper.AnalysisScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7834742Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/CalendarScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'CalendarScraper.CalendarScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7842606Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/EsgScraperTests.cs(20,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'EsgScraper.EsgScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7850431Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HoldersScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HoldersScraper.HoldersScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7864618Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/FundamentalsScraperTests.cs(47,27): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'FundamentalsScraper.FundamentalsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7873145Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(37,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7881328Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/OptionsScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'OptionsScraper.OptionsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7889388Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7897091Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(246,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7904886Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(35,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7912592Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(43,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7920763Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(282,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7928881Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(580,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7937222Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(588,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7951200Z 
2025-12-30T03:54:30.7959506Z Build FAILED.
2025-12-30T03:54:30.7959792Z 
2025-12-30T03:54:30.7966708Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/SharesScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'SharesScraper.SharesScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7975059Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/AnalysisScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'AnalysisScraper.AnalysisScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7983563Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/CalendarScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'CalendarScraper.CalendarScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.7992381Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/EsgScraperTests.cs(20,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'EsgScraper.EsgScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8000501Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HoldersScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HoldersScraper.HoldersScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8009176Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/FundamentalsScraperTests.cs(47,27): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'FundamentalsScraper.FundamentalsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8025407Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(37,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8033719Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/OptionsScraperTests.cs(22,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'OptionsScraper.OptionsScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8041793Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(21,24): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8050243Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(246,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8058484Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(35,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8066595Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs(43,17): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'QuoteScraper.QuoteScraper(IYahooFinanceClient, IDataParser, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8075222Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(282,31): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8084062Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(580,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8092850Z ##[error]/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(588,23): error CS7036: There is no argument given that corresponds to the required parameter 'symbolValidator' of 'HistoryScraper.HistoryScraper(IYahooFinanceClient, IDataParser, IPriceRepair, ITimezoneHelper, ISymbolValidator)' [/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj]
2025-12-30T03:54:30.8097034Z     0 Warning(s)
2025-12-30T03:54:30.8097404Z     15 Error(s)
2025-12-30T03:54:30.8097851Z 
2025-12-30T03:54:30.8098081Z Time Elapsed 00:00:10.58
2025-12-30T03:54:30.8615551Z ##[error]Process completed with exit code 1.
2025-12-30T03:54:30.8695462Z ##[group]Run actions/upload-artifact@v4
2025-12-30T03:54:30.8695757Z with:
2025-12-30T03:54:30.8695942Z   name: test-results
2025-12-30T03:54:30.8696148Z   path: **/TestResults/*.trx

2025-12-30T03:54:30.8696375Z   retention-days: 30
2025-12-30T03:54:30.8696571Z   if-no-files-found: warn
2025-12-30T03:54:30.8696789Z   compression-level: 6
2025-12-30T03:54:30.8696986Z   overwrite: false
2025-12-30T03:54:30.8697198Z   include-hidden-files: false
2025-12-30T03:54:30.8697412Z env:
2025-12-30T03:54:30.8698637Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T03:54:30.8698897Z ##[endgroup]
2025-12-30T03:54:31.1258792Z ##[warning]No files were found with the provided path: **/TestResults/*.trx. No artifacts will be uploaded.
2025-12-30T03:54:31.1390265Z ##[group]Run dorny/test-reporter@v1
2025-12-30T03:54:31.1390548Z with:
2025-12-30T03:54:31.1390722Z   name: Test Results
2025-12-30T03:54:31.1390933Z   path: **/TestResults/*.trx
2025-12-30T03:54:31.1391157Z   reporter: dotnet-trx
2025-12-30T03:54:31.1391354Z   fail-on-error: false
2025-12-30T03:54:31.1391572Z   path-replace-backslashes: false
2025-12-30T03:54:31.1391801Z   list-suites: all
2025-12-30T03:54:31.1391989Z   list-tests: all
2025-12-30T03:54:31.1392172Z   max-annotations: 10
2025-12-30T03:54:31.1392381Z   fail-on-empty: true
2025-12-30T03:54:31.1392579Z   only-summary: false
2025-12-30T03:54:31.1392881Z   token: ***
2025-12-30T03:54:31.1393054Z env:
2025-12-30T03:54:31.1393255Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T03:54:31.1393477Z ##[endgroup]
2025-12-30T03:54:31.2565601Z Action was triggered by pull_request: using SHA from head of source branch
2025-12-30T03:54:31.2577901Z Check runs will be created with SHA=e3551ca9f3052ddc49ecbb3cc35653a98f35890d
2025-12-30T03:54:31.2582008Z ##[group]Listing all files tracked by git
2025-12-30T03:54:31.2628571Z [command]/usr/bin/git ls-files -z
2025-12-30T03:54:31.2768186Z .github/workflows/ci.yml .github/workflows/claude-auto-fix.yml .github/workflows/claude-code-review.yml .github/workflows/claude.yml .gitignore CLAUDE.md PR_DESCRIPTION.md README.md SECURITY_FIX_SUMMARY.md YFinance.NET.Implementation/CalendarService.cs YFinance.NET.Implementation/Constants/YahooFinanceConstants.cs YFinance.NET.Implementation/DependencyInjection/ServiceCollectionExtensions.cs YFinance.NET.Implementation/DomainService.cs YFinance.NET.Implementation/IsinService.cs YFinance.NET.Implementation/LiveMarketService.cs YFinance.NET.Implementation/MarketService.cs YFinance.NET.Implementation/MultiTickerService.cs YFinance.NET.Implementation/Properties/AssemblyInfo.cs YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs YFinance.NET.Implementation/Scrapers/CalendarScraper.cs YFinance.NET.Implementation/Scrapers/CalendarVisualizationScraper.cs YFinance.NET.Implementation/Scrapers/EarningsScraper.cs YFinance.NET.Implementation/Scrapers/EsgScraper.cs YFinance.NET.Implementation/Scrapers/FastInfoScraper.cs YFinance.NET.Implementation/Scrapers/FundamentalsScraper.cs YFinance.NET.Implementation/Scrapers/FundsScraper.cs YFinance.NET.Implementation/Scrapers/HistoryScraper.cs YFinance.NET.Implementation/Scrapers/HoldersScraper.cs YFinance.NET.Implementation/Scrapers/InfoScraper.cs YFinance.NET.Implementation/Scrapers/LookupScraper.cs YFinance.NET.Implementation/Scrapers/NewsScraper.cs YFinance.NET.Implementation/Scrapers/OptionsScraper.cs YFinance.NET.Implementation/Scrapers/QuoteScraper.cs YFinance.NET.Implementation/Scrapers/ScreenerScraper.cs YFinance.NET.Implementation/Scrapers/SearchScraper.cs YFinance.NET.Implementation/Scrapers/SharesScraper.cs YFinance.NET.Implementation/Services/CacheService.cs YFinance.NET.Implementation/Services/CookieService.cs YFinance.NET.Implementation/Services/RateLimitService.cs YFinance.NET.Implementation/TickerService.cs YFinance.NET.Implementation/Tickers.cs YFinance.NET.Implementation/Utils/DataParser.cs YFinance.NET.Implementation/Utils/JsonElementExtensions.cs YFinance.NET.Implementation/Utils/PriceRepair.cs YFinance.NET.Implementation/Utils/SymbolValidator.cs YFinance.NET.Implementation/Utils/TimezoneHelper.cs YFinance.NET.Implementation/YFinance.NET.Implementation.csproj YFinance.NET.Implementation/YahooFinanceClient.cs YFinance.NET.Interfaces/ICalendarService.cs YFinance.NET.Interfaces/IDomainService.cs YFinance.NET.Interfaces/IIsinService.cs YFinance.NET.Interfaces/ILiveMarketService.cs YFinance.NET.Interfaces/IMarketService.cs YFinance.NET.Interfaces/IMultiTickerService.cs YFinance.NET.Interfaces/ITickerService.cs YFinance.NET.Interfaces/IYahooFinanceClient.cs YFinance.NET.Interfaces/Scrapers/IAnalysisScraper.cs YFinance.NET.Interfaces/Scrapers/ICalendarScraper.cs YFinance.NET.Interfaces/Scrapers/ICalendarVisualizationScraper.cs YFinance.NET.Interfaces/Scrapers/IEarningsScraper.cs YFinance.NET.Interfaces/Scrapers/IEsgScraper.cs YFinance.NET.Interfaces/Scrapers/IFastInfoScraper.cs YFinance.NET.Interfaces/Scrapers/IFundamentalsScraper.cs YFinance.NET.Interfaces/Scrapers/IFundsScraper.cs YFinance.NET.Interfaces/Scrapers/IHistoryScraper.cs YFinance.NET.Interfaces/Scrapers/IHoldersScraper.cs YFinance.NET.Interfaces/Scrapers/IInfoScraper.cs YFinance.NET.Interfaces/Scrapers/ILookupScraper.cs YFinance.NET.Interfaces/Scrapers/INewsScraper.cs YFinance.NET.Interfaces/Scrapers/IOptionsScraper.cs YFinance.NET.Interfaces/Scrapers/IQuoteScraper.cs YFinance.NET.Interfaces/Scrapers/IScreenerScraper.cs YFinance.NET.Interfaces/Scrapers/ISearchScraper.cs YFinance.NET.Interfaces/Scrapers/ISharesScraper.cs YFinance.NET.Interfaces/Services/ICacheService.cs YFinance.NET.Interfaces/Services/ICookieService.cs YFinance.NET.Interfaces/Services/IRateLimitService.cs YFinance.NET.Interfaces/Utils/IDataParser.cs YFinance.NET.Interfaces/Utils/IPriceRepair.cs YFinance.NET.Interfaces/Utils/ISymbolValidator.cs YFinance.NET.Interfaces/Utils/ITimezoneHelper.cs YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj YFinance.NET.Models/ActionData.cs YFinance.NET.Models/ActionsData.cs YFinance.NET.Models/AnalystData.cs YFinance.NET.Models/CalendarData.cs YFinance.NET.Models/CalendarQuery.cs YFinance.NET.Models/CalendarRequest.cs YFinance.NET.Models/CalendarResult.cs YFinance.NET.Models/DomainData.cs YFinance.NET.Models/EarningsData.cs YFinance.NET.Models/Enums/Interval.cs YFinance.NET.Models/Enums/LookupType.cs YFinance.NET.Models/Enums/Period.cs YFinance.NET.Models/Enums/StatementType.cs YFinance.NET.Models/EsgData.cs YFinance.NET.Models/Exceptions/DataParsingException.cs YFinance.NET.Models/Exceptions/InvalidTickerException.cs YFinance.NET.Models/Exceptions/RateLimitException.cs YFinance.NET.Models/Exceptions/YahooFinanceException.cs YFinance.NET.Models/FastInfo.cs YFinance.NET.Models/FastInfoData.cs YFinance.NET.Models/FinancialStatement.cs YFinance.NET.Models/FundsData.cs YFinance.NET.Models/HistoricalData.cs YFinance.NET.Models/HistoryMetadata.cs YFinance.NET.Models/HolderData.cs YFinance.NET.Models/InfoData.cs YFinance.NET.Models/LivePriceData.cs YFinance.NET.Models/LookupResult.cs YFinance.NET.Models/MajorHoldersData.cs YFinance.NET.Models/NewsItem.cs YFinance.NET.Models/OptionsData.cs YFinance.NET.Models/QuoteData.cs YFinance.NET.Models/RecommendationData.cs YFinance.NET.Models/Requests/ActionsRequest.cs YFinance.NET.Models/Requests/EarningsDatesRequest.cs YFinance.NET.Models/Requests/HistoryRequest.cs YFinance.NET.Models/Requests/LookupRequest.cs YFinance.NET.Models/Requests/NewsRequest.cs YFinance.NET.Models/Requests/OptionChainRequest.cs YFinance.NET.Models/Requests/ScreenerRequest.cs YFinance.NET.Models/Requests/SearchRequest.cs YFinance.NET.Models/Requests/SharesHistoryRequest.cs YFinance.NET.Models/ScreenerPredefinedQueries.cs YFinance.NET.Models/ScreenerQuery.cs YFinance.NET.Models/ScreenerResult.cs YFinance.NET.Models/SearchResult.cs YFinance.NET.Models/SharesData.cs YFinance.NET.Models/YFinance.NET.Models.csproj YFinance.NET.Tests/Integration/TickerServiceIntegrationTests.cs YFinance.NET.Tests/TestFixtures/MockHttpMessageHandler.cs YFinance.NET.Tests/TestFixtures/MockYahooFinanceClient.cs YFinance.NET.Tests/TestFixtures/TestDataBuilder.cs YFinance.NET.Tests/Unit/MultiTickerServiceTests.cs YFinance.NET.Tests/Unit/Scrapers/AnalysisScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/CalendarScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/EarningsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/EsgScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/FundamentalsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/FundsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/HoldersScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/LookupScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/NewsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/OptionsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/ScreenerScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/SearchScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/SharesScraperTests.cs YFinance.NET.Tests/Unit/Services/RateLimitServiceTests.cs YFinance.NET.Tests/Unit/TickerServiceTests.cs YFinance.NET.Tests/Unit/Utils/DataParserTests.cs YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs YFinance.NET.Tests/Unit/YahooFinanceClientTests.cs YFinance.NET.Tests/YFinance.NET.Tests.csproj YFinance.NET.sln build.log global.json 
2025-12-30T03:54:31.2818909Z ##[endgroup]
2025-12-30T03:54:31.2819372Z Found 159 files tracked by GitHub
2025-12-30T03:54:31.2819910Z Using test report parser 'dotnet-trx'
2025-12-30T03:54:31.2843725Z ##[group]Creating test report Test Results
2025-12-30T03:54:31.2847354Z ##[warning]No file matches path **/TestResults/*.trx
2025-12-30T03:54:31.2849275Z ##[endgroup]
2025-12-30T03:54:31.2860347Z ##[error]No test report files were found
2025-12-30T03:54:31.3044013Z Post job cleanup.
2025-12-30T03:54:31.4024633Z [command]/usr/bin/git version
2025-12-30T03:54:31.4073086Z git version 2.52.0
2025-12-30T03:54:31.4117903Z Temporarily overriding HOME='/home/runner/work/_temp/56735674-3274-4360-bd44-53f92305650e' before making global git config changes
2025-12-30T03:54:31.4119389Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T03:54:31.4124328Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T03:54:31.4160875Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T03:54:31.4195872Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T03:54:31.4443840Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T03:54:31.4467213Z http.https://github.com/.extraheader
2025-12-30T03:54:31.4480590Z [command]/usr/bin/git config --local --unset-all http.https://github.com/.extraheader
2025-12-30T03:54:31.4512667Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T03:54:31.4751375Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T03:54:31.4785305Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T03:54:31.5132375Z Cleaning up orphan processes
2025-12-30T03:54:31.5432077Z Terminate orphan process: pid (2338) (dotnet)
2025-12-30T03:54:31.5451576Z Terminate orphan process: pid (2339) (dotnet)
2025-12-30T03:54:31.5479677Z Terminate orphan process: pid (2340) (dotnet)
2025-12-30T03:54:31.5509175Z Terminate orphan process: pid (2462) (VBCSCompiler)

```

