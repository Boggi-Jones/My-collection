import unittest
import os, sys
dir_path = os.path.dirname(os.path.realpath(__file__))
parent_dir_path = os.path.abspath(os.path.join(dir_path, os.pardir))
sys.path.insert(0, parent_dir_path)
from app.Functions.room_functions import RoomFunctions
from app.Models.roomclass import Room

class TestRoomFunctions(unittest.TestCase):
    def setUp(self):
        self.room_functions = RoomFunctions()

    def test_book_room(self):
        ''''Creates a room with the name "101", books it at 8:00, and attempts to look up the room booking.'''
        room = Room("101")
        self.room_functions.rooms.append(room)
        self.room_functions.book_room(room, 0, "20/09/2021")

        self.assertEqual(self.room_functions.room_bookings["20/09/2021"], [["101", "8:00"]])

    def test_view_room_bookings(self):
        '''Creates a room with the name "101", books it at 8:00, and verifies that the output of
        the "view_room_bookings" function correctly shows the room booking.'''
        room = Room("101")
        self.room_functions.rooms.append(room)
        self.room_functions.book_room(room, 0, "20/09/2021")
        bookings = self.room_functions.view_room_bookings("20/09/2021")
        
        self.assertEqual(bookings, '[["101", "8:00"]]')

    def tearDown(self):
        print("Testing done")

if __name__ == "__main__":
    unittest.main()