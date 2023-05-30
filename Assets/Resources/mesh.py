import math
import numpy as np
from scipy.integrate import quad
from random import gauss
import json
#import matplotlib.pyplot as plt

table = [[] for i in range(0, 1901)]





table = []

class point:
    def __init__(self, position, index):
        self.position = position
        self.x = position[0]
        self.y = position[1]
        self.z = position[2]
        self.index = index
        self.connected_points = []


class WavePacket:
    def __init__(self, pulseNum):
        self.pulseNum = pulseNum
        self.points = []
        self.triangles = []
        self.kval = 1

        self.fillPoints()
        self.drawTriangles()


    def exportPoints(self):
        lst = []
        for col in self.points:
            for point in col:
                vertex = {"x": point.x, "y": point.y, "z": point.z}
                lst.append(vertex)
        return lst

    def exportTrianges(self):
        return self.triangles

    def xScaler(self, oldx):

        slope = -(1/200) * self.pulseNum + 0.5
        intercept = (3/4) + (self.pulseNum/400)
        scaledX = slope*oldx + intercept
        return scaledX



    def gaussian(self, z):
        c = 700
        g = math.exp(-(z ** 2) / (2 * c ** 2))
        return g

    def integrand(self, k, x):
        return math.exp((-(k-self.kval)**2/.4**2)) * np.cos((0.10*k**2 + .75*k)*(x/50+1.07*self.pulseNum) - k*self.pulseNum)

    def packet(self, x):
        offset = self.kval - .001
        p = 0
        for i in range(1, 11):
            p += (quad(self.integrand, self.kval - offset * i / 10, self.kval - offset * (i - 1) / 10, (x))[0])
            p += (quad(self.integrand, self.kval + offset * (i - 1) / 10, self.kval + offset * i / 10, (x))[0])
        return abs(p)


    def wave(self, x, z):
        return 1000*self.gaussian(z) * self.packet(x)

    def fillPoints(self):
        index = 0
        for x in range(-1050, 1050, 20):
            col = []
            for z in range(0, 2500, 20):
                peak = self.wave(x, 0)
                y = 100*self.wave(x, z)
                if y > 50 or z <= 500*peak**(1/10):
                    col.append(point((x, y, z), index))
                    index += 1
            self.points.append(col)

    def drawTriangles(self):
        for i in range(0, len(self.points)-1, 1):
            currentcol = self.points[i]
            nextcol = self.points[i+1]
            toppointnextcolreached = len(nextcol) -1
            for j in range(len(currentcol)-1, 0, -1):
                current_point = currentcol[j]
                while j <= toppointnextcolreached:
                    self.triangles.append(current_point.index)
                    self.triangles.append(nextcol[toppointnextcolreached].index)
                    self.triangles.append(nextcol[toppointnextcolreached-1].index)

                    # self.triangles.append(nextcol[toppointnextcolreached].index)
                    # self.triangles.append(current_point.index)
                    # self.triangles.append(nextcol[toppointnextcolreached - 1].index)

                    toppointnextcolreached -= 1
                # draw downward-starting triangle
                self.triangles.append(current_point.index)
                self.triangles.append(nextcol[toppointnextcolreached].index)
                self.triangles.append(current_point.index-1)

                # self.triangles.append(nextcol[toppointnextcolreached].index)
                # self.triangles.append(current_point.index)
                # self.triangles.append(current_point.index - 1)



    def findUpNeighbor(self, point):
        if point.index +1 == len(self.points):
            return False

        upNeighbor = self.points[point.index+1]
        if upNeighbor.x == point.x:
            return upNeighbor.index


for i in range(0,101, 10):

    wp = WavePacket(i)
    print(i)
    wavepacket = {"vertices": wp.exportPoints(),
                  "triindexes" : wp.exportTrianges()}
    f = open("surface{}.json".format(str(i)), "x")

    with open("surface{}.json".format(str(i)), "w") as jfile:
        json.dump(wavepacket, jfile)



#print(wp.exportPoints()[0:10])
#print(wp.triangles)

