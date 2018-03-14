import numpy as np
import array

with open("IntegerArray.txt", "r") as tab:
    array = []
    arraySorted = []
    for line in tab:
        array.append(line)

arraySize = len(array)

for i in range(0, arraySize-1):
    if (array[i+1] < array[i]):
        valeurTemp = array[i]
        array[i] = array[i+1]
        array[i+1] = valeurTemp
arraySorted.append(array[i])
array.remove(array[i])

arraySortedSize = len(arraySorted)
# Vérif. taille du tableau 
print(arraySortedSize)

for i in range(0, arraySortedSize-1):
    print(arraySorted[i])

    
    
