import time
import datetime
class Appointment:
    def __init__(self, doctor_id, patient_id, datetime_object, time_object):
        self.doctor_id = doctor_id
        self.patient_id = patient_id
        self.date = datetime_object
        self.time = time_object

    def __str__(self):
        '''
        Returns Appointment object as a string.
        '''
        return '''
        ---------------------------
        doctor id: {}
        patient id: {}
        date: {}
        time: {}
        ---------------------------
        '''.format(str(self.doctor_id),
        str(self.patient_id),
        str(self.date),
        str(self.time))
