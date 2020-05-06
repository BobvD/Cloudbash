import { Injectable } from '@angular/core';
import { Config } from '../models/config.model';

@Injectable({
    providedIn: 'root',
  })
export class ConfigService {

    configs: Config[] = [
        { id: 1, name: "Config 1", apiUrl: "https://hwhewc41ge.execute-api.us-east-1.amazonaws.com/dev/", eventBusType: "DynamoDB Streams", eventStoreType: "DynamoDB", readDatabaseType: "DynamoDB", documentationUrl: "" },
        { id: 2, name: "Config 2", apiUrl: "",eventBusType: "Kinesis", eventStoreType: "DynamoDB", readDatabaseType: "RDS (Postgres)", documentationUrl: "" },        
        { id: 3, name: "Config 3", apiUrl: "", eventBusType: "SQS", eventStoreType: "DynamoDB", readDatabaseType: "ElastiCache (Redis)", documentationUrl: "" },
        { id: 4, name: "custom", apiUrl: "", eventBusType: "Unknown", eventStoreType: "Unknown", readDatabaseType: "Unknown", documentationUrl: "" }           
        
    ]

    defaultConfig = 1;
    currentConfig = this.defaultConfig;
    
    
    getApiBaseUrl() : string{
        return localStorage.getItem('apiUrl');  
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

}