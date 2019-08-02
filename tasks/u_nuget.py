
import glob
import re
from lib.nuget_update import NugetUpdate


def main():
    nuget_updater = NugetUpdate()
    nuget_updater.get_latest_ver()
    nuget_updater.update_nuget()

if __name__ == '__main__':
    main()