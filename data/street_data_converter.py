import sys
import csv
import json
import datetime

JURISDICTIONS = {1 #alexandraia city
                 ,4 #farifax county
                 ,5 #fairfax city
                 }

def isjurd(j1 : int, j2 : int) -> bool:
    return j1 in JURISDICTIONS or j2 in JURISDICTIONS

streetdistinct = set()
streetarray=[]
with open(sys.argv[1], 'r', encoding='utf-8', newline='') as reader:
    creader = csv.DictReader(reader)
    for arow in creader:
        fn = arow['FULLNAME'].upper()
        int(arow['L_JURISDICTION'])
        if isjurd(int(arow['L_JURISDICTION']), int(arow['R_JURISDICTION'])) and (fn not in streetdistinct) :
            tdict = {'zipcode_range' : (int(arow['L_POSTAL_AREA']), int(arow['R_POSTAL_AREA'])), 'name' : fn  }
            streetarray.append(tdict)
            streetdistinct.add(fn)

output_struct = {'update_date' : datetime.datetime.now().isoformat(), 'data' : sorted(streetarray, key=lambda k: k['name']) }

with open('streets.json', 'w', encoding='utf-8') as writer:
    json.dump(output_struct, writer)