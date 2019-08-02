import os
import shutil
import os.path
import subprocess
import time
import json
import logging
import re
import glob

class NugetUpdate:

    def get_nutpegs(self):
        print('looking for nutpegs in ' + self.domain_lib_path)
        self.domain_nutpegs = [os.path.basename(x) for x in glob.glob(self.domain_lib_path + "*.nupkg")]
        self.domain_nutpegs.sort()
        print(self.domain_nutpegs)

        print('looking for nutpegs in ' + self.mongodb_lib_path)
        self.mongo_nutpegs = [os.path.basename(x) for x in glob.glob(self.mongodb_lib_path + "*.nupkg")]
        self.mongo_nutpegs.sort()
        print(self.mongo_nutpegs)

    def get_latest_ver(self):
        regex = '(?=\.\d)\.([^[aZ-zZ]+)\.(?!\.nupkg)'

        for n in self.domain_nutpegs:
            nr = re.search(regex, n)
            print(nr.group(1))
            self.domain_lib_versions.append(nr.group(1))

        # pre-sort
        # print('domain_lib_versions pre-sort\r\n') 
        # print(self.domain_lib_versions)

        # sort ascending
        self.domain_lib_versions.sort()
        
        # post-sort
        # print('domain_lib_versions post-sort\r\n') 
        # print(self.domain_lib_versions)
                
        self.domain_lib_last_version = self.domain_lib_versions[-1]
        print('domain_lib_last_version ' + self.domain_lib_last_version)

        for m in self.mongo_nutpegs:
            mr = re.search(regex, m)
            print(mr.group(1))
            self.mongodb_lib_versions.append(mr.group(1))
        # sort ascending
        self.mongodb_lib_versions.sort()
        self.mongodb_lib_last_version = self.mongodb_lib_versions[-1]
        print('mongodb_lib_last_version ' + self.mongodb_lib_last_version)

    def update_nuget(self):
        zatoichi_eventsourcing_nuget = self.domain_lib_path + 'Zatoichi.EventSourcing.' + self.domain_lib_last_version + '.nupkg'
        zatoichi_eventsourcing_mongodb_nuget = self.mongodb_lib_path + 'Zatoichi.EventSourcing.MongoDb.' + self.mongodb_lib_last_version + '.nupkg'
        
        push = ['dotnet', 'nuget', 'push', '-s', 'http://zatoichi.ddns.net:8080/v3/index.json', '-k', 'K0te_12345', zatoichi_eventsourcing_nuget]
        print(push)
        subprocess.run(push)
        push = push[:-1]
        push.append(zatoichi_eventsourcing_mongodb_nuget)
        print(push)
        subprocess.run(push)

    def __init__(self):
        self.domain_lib_path =  'c:/dev/Zatoichi/Zatoichi.EventSourcing/Zatoichi.EventSourcing/bin/Release/'
        self.mongodb_lib_path = 'c:/dev/Zatoichi/Zatoichi.EventSourcing/Zatoichi.EventSourcing.MongoDb/bin/Release/'
                                
        self.domain_lib_last_version = ''
        self.mongodb_lib_last_version = ''
        self.domain_lib_versions = []
        self.mongodb_lib_versions = []
        self.get_nutpegs()
            




