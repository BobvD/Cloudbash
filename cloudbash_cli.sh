#!/bin/bash
# Bash Menu Script Example

bold=$(tput bold)
normal=$(tput sgr0)

title="Which Serverles Framework configuration do you want to deploy?\n\n"
prompt="Pick an option:"
options=(
    "Eventstream: ${bold}Kinesis${normal}, Event store: ${bold}DynamoDB${normal}, Read Database: ${bold}RDS (Postgres)${normal}" 
    "Eventstream: ${bold}Simple Queue Service${normal}, Event store: ${bold}DynamoDB${normal}, Read Database: ${bold}ElastiCache (Redis)${normal}"
    "Eventstream: ${bold}DynamoDB Streams${normal}, Event store: ${bold}DynamoDB${normal}, Read Database: ${bold}DynamoDB${normal}"
    "Amazon Cognito"
    "${bold}Remove${normal} an existing deployment"
     )

removeOptions=(
    "Configuration #1"
    "Configuration #2"
    "Configuration #3",
    "Cognito"
)

printf "\e[95m"
printf "\n\n"
FILE="./documentation/assets/ascii.txt"
cat $FILE
printf $normal
printf "\nNote: you need to deploy the cognito user pool before deploying any configuration.\n\n"
printf "$title"

SERVERLESS_CONFIGURATION_1="serverless.yml"
SERVERLESS_CONFIGURATION_2="serverless.sqs.yml"
SERVERLESS_CONFIGURATION_3="serverless.dynamodb.yml"
SERVERLESS_CONFIGURATION_4="serverless.cognito.yml"
START_MSG="Starting serverless... \n\n"
BUILD_MSG="Building source code... \n\n"

PS3="$prompt "
cd src/Lambda; 
select opt in "${options[@]}" "Quit"; do 

    case "$REPLY" in

    1 ) printf "\e[95m\nOption 1 selected\n${normal}";
        printf "$BUILD_MSG"; 
        ./build.sh; 
        printf "$START_MSG"; 
        sls deploy --config $SERVERLESS_CONFIGURATION_1; 
        break;;
    2 ) printf "\e[95m\nOption 2 selected\n${normal}"; 
        printf "$BUILD_MSG"; 
        ./build.sh; 
        printf "$START_MSG"; 
        sls deploy --config $SERVERLESS_CONFIGURATION_2; 
        break;;
    3)  printf "\e[95m\nOption 3 selected\n${normal}"; 
        printf "$BUILD_MSG"; 
        ./build.sh; 
        printf "$START_MSG"; 
        sls deploy --config $SERVERLESS_CONFIGURATION_3; 
        break;;
    4 ) printf "\e[95m\nOption 4 selected\n${normal}"; 
        printf "$START_MSG"; 
        sls deploy --config $SERVERLESS_CONFIGURATION_4; 
        break;;
    5 ) printf "\nWhich deployment do you want to remove?\n\n";

        select opt in "${removeOptions[@]}" "Quit"; do 

            case "$REPLY" in
            1 ) printf "\e[95mStarting to remove configuration #1...${normal}\n"; 
                sls remove --config $SERVERLESS_CONFIGURATION_1; 
                break;;
            2 ) printf "\e[95mStarting to remove configuration #2...${normal}\n"; 
                sls remove --config $SERVERLESS_CONFIGURATION_2;
                break;;
            3 ) printf "\e[95mStarting to remove configuration #3...${normal}\n"; 
                sls remove --config $SERVERLESS_CONFIGURATION_3;
                break;;
            4 ) printf "\e[95mStarting to remove cognito user pool #3...${normal}\n"; 
                sls remove --config $SERVERLESS_CONFIGURATION_4;
                break;; 
            5 ) echo "Goodbye!"; break;;
            *) echo "Invalid option. Try another one.";continue;;

            esac

        done
        break;;
    $(( ${#options[@]}+1 )) ) echo "Goodbye!"; break;;
    *) echo "Invalid option. Try another one.";continue;;

    esac

done
