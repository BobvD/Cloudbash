import { Injectable } from '@angular/core';
import { Config } from '../models/config.model';
import {webSocket, WebSocketSubject} from 'rxjs/webSocket';

@Injectable({
    providedIn: 'root',
  })
export class ConfigService {

    configs: Config[] = [
        { id: 1, name: "Config 1", apiUrl: "https://u6ah3ubpa0.execute-api.us-east-1.amazonaws.com/dev", websocketUrl: "wss://4pcnw9kl9b.execute-api.us-east-1.amazonaws.com/dev/", eventBusType: "DynamoDB Streams", eventStoreType: "DynamoDB", readDatabaseType: "DynamoDB", documentationUrl: "" },
        { id: 2, name: "Config 2", apiUrl: "https://z65ppv9o49.execute-api.us-east-1.amazonaws.com/dev/",eventBusType: "Kinesis", eventStoreType: "DynamoDB", websocketUrl: "", readDatabaseType: "RDS (Postgres)", documentationUrl: "" },        
        { id: 3, name: "Config 3", apiUrl: "", eventBusType: "SQS", eventStoreType: "DynamoDB", websocketUrl: "", readDatabaseType: "ElastiCache (Redis)", documentationUrl: "" },
        { id: 4, name: "custom", apiUrl: "", eventBusType: "Unknown", eventStoreType: "Unknown", websocketUrl: "", readDatabaseType: "Unknown", documentationUrl: "" }           
        
    ]

    defaultConfig = 2;
    currentConfig = this.defaultConfig;
    
    myWebSocket: WebSocketSubject<any>;

    getApiBaseUrl() : string{
        return localStorage.getItem('apiUrl');  
    }  

    private getWebSocketUrl(): string {
        return this.getConfig(this.currentConfig).websocketUrl;
    }

    getConfig(id: number) : Config {
        return this.configs.find(c => c.id === id);        
    } 

    getAllConfigs(): Config[] {
        return this.configs;
    }

    setConfig(id: number) {
        const config = this.getConfig(id);
        if(config) {
            localStorage.setItem('config_id', config.id.toString());  
            localStorage.setItem('apiUrl', config.apiUrl);  
        }
    }

    setApiUrl(apiUrl: string){
        const config = this.getConfig(4);
        config.apiUrl = apiUrl;
        localStorage.setItem('apiUrl', apiUrl);  
    }

    getConfigFromStorage() : Config {
        let id = +localStorage.getItem('config_id');
        if(!id) {
            this.setConfig(1);
            id = 1;
        }
        const config = this.getConfig(id);
        if(config.id == 4) {
            config.apiUrl = this.getApiBaseUrl();
        }
        return config;
    } 

    connectToEventStore() {
        /*
        this.myWebSocket = webSocket(this.getWebSocketUrl());
        this.myWebSocket.subscribe(    
            msg => console.log('message received: ' + msg), 
            // Called whenever there is a message from the server    
            err => console.log(err), 
            // Called if WebSocket API signals some kind of error    
            () => console.log('complete') 
            // Called when connection is closed (for whatever reason)  
         );

        this.myWebSocket.asObservable().subscribe(dataFromServer => {
            console.log("connected")
            console.log(dataFromServer);
        });
        */
    }

    getS3BucketUrl() {
        return `https://cb-c${this.currentConfig}.s3.amazonaws.com/`
    }

}