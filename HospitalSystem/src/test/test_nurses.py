import unittest
import os, sys
dir_path = os.path.dirname(os.path.realpath(__file__))
parent_dir_path = os.path.abspath(os.path.join(dir_path, os.pardir))
sys.path.insert(0, parent_dir_path)
from app.Functions.nurse_functions import NurseFunctions

class TestNurses(unittest.TestCase):

    def test_add_nurses(self):
        ''' Add some information trough an input and test if the function adds the information to the list'''
        self.my_data = NurseFunctions()
        self.my_data.add_nurses(1308971234,"Alice", 5432123)
        self.my_data.add_nurses(1642345642, "Ben", 1254367)

        print(self.my_data)
        ''' Make sure that the information added is what it is supposed to be'''
        self.assertEqual(self.my_data.nurses[0], {1308971234: ["Alice", 5432123]})
        self.assertEqual(self.my_data.nurse[1642345642], ["Ben", 1254367])

if __name__ == "__main__":
    unittest.main()