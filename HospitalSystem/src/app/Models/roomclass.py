class Room:
    def __init__(self, room_name: str):
        self.room_name = room_name
        self.time_slots = []
        self.generate_time_slots()
        
    def generate_time_slots(self):
        curr_hour = 8
        curr_min_dozen = 0
        while curr_hour < 24:
            self.time_slots.append(str(curr_hour) + ":" + str(curr_min_dozen) + "0")
            if curr_min_dozen == 0:
                curr_min_dozen = 3
            else:
                curr_min_dozen = 0
                curr_hour += 1