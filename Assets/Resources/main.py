import math
import numpy
from scipy.integrate import quad
from random import gauss

file = open("surface.txt", "w")
file.truncate(0)
points = []


def integrand(w, x):
    return math.cos(x * w * math.pi / 180)


def recordpoint(point, fileobj):

    line = f'{point[0]}\t{point[2]}\t{point[1]}\n'
    fileobj.write(line)


def gaussian(peak_val, z):
    c = 700
    y = peak_val* math.exp(-(z**2)/(2*c**2))
    return y


count = 0
table = []
for i in range(-1000, 1, 20):
    x = i
    peak_y = abs(2500*quad(integrand, 0.25, 1, x)[0])
    y = abs(2500*quad(integrand, 0.25, 1, x)[0])
    z = 0
    recordpoint((x, y, z), file)
    count += 1

    while y > 50:
        z += 20
        y = gaussian(peak_y, z)
        recordpoint((x, y, z), file)
        #recordpoint((x, y, -z), file)
        count += 1





print(count)
file.close()
