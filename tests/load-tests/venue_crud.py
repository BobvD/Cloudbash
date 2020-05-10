from locust import HttpLocust, TaskSet, task
import json
 
class UserBehavior(TaskSet):
 
    @task(1)    
    def create_concert(self):
        headers = {'content-type': 'application/json','Accept-Encoding':'gzip'}
        self.client.post("/venues",data= json.dumps({
      "Name": "test-string",
      "Address": "test-address"
    }), 
    headers=headers, 
    name = "Create a new concert") 
 
 
class WebsiteUser(HttpLocust):
    task_set = UserBehavior