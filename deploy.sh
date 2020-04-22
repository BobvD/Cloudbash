#!/bin/bash
# Bash Menu Script Example

bold=$(tput bold)
normal=$(tput sgr0)

title="Which Serverles Framework configuration do you want to deploy?\n\n"
prompt="Pick an option:"
options=(
    "Eventstream: ${bold}Kinesis${normal}, Event store: ${bold}DynamoDB${normal}, Read Database: ${bold}RDS (Postgres)${normal}" 
    "Eventstream: ${bold}Simple Queue Service${normal}, Event store: ${bold}DynamoDB${normal}, Read Database: ${bold}ElastiCache (Redis)${normal}"
     "${bold}Remove${normal} an existing deployment"
     )

removeOptions=(
    "Configuration #1"
    "Configuration #2"
)

printf "\e[95m"
printf "\n\n"
FILE="./documentation/assets/ascii.txt"
cat $FILE
printf $normal
printf "\n"

printf "$title"

SERVERLESS_CONFIGURATION_1="serverless.yml"
SERVERLESS_CONFIGURATION_2="serverless.sqs.yml"

PS3="$prompt "
cd src/Lambda; 
select opt in "${options[@]}" "Quit"; do 

    case "$REPLY" in

    1 ) printf "\e[95m\nOption 1 selected\n${normal}";
        printf "Building source code... \n\n"; 
        ./build.sh; 
        printf "\n\nStarting serverless... \n\n"; 
        sls deploy --config $SERVERLESS_CONFIGURATION_1; 
        break;;
    2 ) printf "\e[95m\nOption 2 selected\n${normal}"; 
        printf "Building source code... \n\n"; 
        ./build.sh; 
        printf "\n\nStarting serverless... \n\n"; 
        sls deploy --config $SERVERLESS_CONFIGURATION_2; 
        break;;
    3 ) printf "\nWhich deployment do you want to remove?\n\n";

        select opt in "${removeOptions[@]}" "Quit"; do 

            case "$REPLY" in
            1 ) printf "\e[95mStarting to remove configuration #1...${normal}\n"; 
                sls remove --config $SERVERLESS_CONFIGURATION_1; 
                break;;
            2 ) printf "\e[95mStarting to remove configuration #2...${normal}\n"; 
                sls remove --config $SERVERLESS_CONFIGURATION_2;
                break;;
            esac

        done


        break;;
    $(( ${#options[@]}+1 )) ) echo "Goodbye!"; break;;
    *) echo "Invalid option. Try another one.";continue;;

    esac

done
