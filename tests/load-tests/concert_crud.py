from locust import HttpLocust, TaskSet, task
import json
 
class UserBehavior(TaskSet):
 
    @task(1)    
    def create_concert(self):
        headers = {'content-type': 'application/json','Accept-Encoding':'gzip'}
        self.client.post("/concerts",data= json.dumps({
      "tinametle": "test-string",
    }), 
    headers=headers, 
    name = "Create a new concert")
 
    @task(1)    
    def list_concert(self):
        headers = {'content-type': 'application/json','Accept-Encoding':'gzip'}
        self.client.get("/concerts", 
    headers=headers, 
    name = "List all concerts")
 
 
class WebsiteUser(HttpLocust):
    task_set = UserBehavior