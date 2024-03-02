import os
import requests
from dotenv import load_dotenv
import argparse
load_dotenv()
MAP_SEARCH_API=f"https://atlas.microsoft.com/search/address/json?&subscription-key={os.environ['AZURE_MAP_ACCOUNT_KEY']}&api-version=1.0&language=en-US"

def makeQuery(address : str):
    url = MAP_SEARCH_API + "&query=" + address
    return requests.get(url=url).json()


argparser = argparse.ArgumentParser(prog='Azure Map Client', description="Azure Map Client 1.0 - programmatic access to Azure maps", add_help=True)
argparser.add_argument('--address', type=str, required=True,help='Address to query for')
options = vars(argparser.parse_args())

assert options['address']

print(makeQuery(options['address']))