import requests

url = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/"

def f5():
    while True:
        try:
            i = url + "g?q=" + id + "&u=0" + "&i=15.52151251%2052.5215216436436"
            print(i)
            jsonXY = requests.get(i).json()
            print(jsonXY)
        except:
            pass
        
def f4():
    while True:
        try:
            u1 = url + "g?q=" + id + "&u=1"
            print(u1)
            jsonU1 = requests.get(u1).json()
            print(jsonU1)

            f5()
        except:
            pass

def f3():
    while True:
        try:
            u0 = url + "g?q=" + id + "&u=0"
            print(u0)
            jsonU0 = requests.get(u0).json()
            print(jsonU0)

            f4()
        except:
            pass

def f2():
    while True:
        try:
            q = url + "g?q=" + id
            print(q)
            jsonQ = requests.get(q).json()
            print(jsonQ)

            f3()
        except:
            pass
        
while True:
    try:
        create = url + "create"
        jsonCreate = requests.get(create).json()

        id = jsonCreate['id']
        print(create)
        print(id)

        f2()
    except:
        pass
