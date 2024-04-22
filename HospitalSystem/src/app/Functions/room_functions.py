import json
from random import randrange

import os, sys
dir_path = os.path.dirname(os.path.realpath(__file__))
parent_dir_path = os.path.abspath(os.path.join(dir_path, os.pardir))
sys.path.insert(0, parent_dir_path)

from Models.roomclass import Room

class RoomFunctions:
    def __init__(self):
        self.rooms = []
        self.room_bookings = {}
        #self.add_dummy_data()

    def view_room_bookings(self, date: str):
        '''Takes in a date, as a str with the format "DD/MM/YY", and returns a json string with the
        bookings that have been made for that day.'''
        return json.dumps(self.room_bookings[date])

    def book_room(self, room: Room, slot: int, date: str):
        '''Takes in an instance of a Room class, an int indicating which time slot is being booked,
        and a date as a str. Removes the time slot from the Room's list of available time slots,
        and adds the room name and time slot to a list of bookings.'''
        booking = [room.room_name, room.time_slots[slot]]
        if date in self.room_bookings:
            self.room_bookings[date].append(booking)
        else:
            self.room_bookings[date] = [booking]
        #print("Room " + str(room.room_name) + " booked at " + str(room.time_slots[slot]))
        room.time_slots.pop(slot)

    def add_dummy_data(self):
        '''A placeholder function that populates the RoomFunctions class with rooms to work with.'''
        self.rooms_nrs = [101, 102, 103, 201, 202, 203, 301, 302, 303]
        
        for room_nr in self.rooms_nrs:
            self.rooms.append(Room(room_nr))

        for room in self.rooms:
            for i in range(0, 3):
                self.book_room(room, randrange(0, len(room.time_slots)), "20/09/2021")
        
        self.view_room_bookings("20/09/2021")

roomfunc = RoomFunctions()