#!/bin/bash
# Bash Menu Script Example

bold=$(tput bold)
normal=$(tput sgr0)

title="Which Serverles Framework configuration do you want to deploy?\n\n"
prompt="Pick an option:"
options=(
    "Eventstream: ${bold}Kinesis${normal}, Event store: ${bold}DynamoDB${normal}, Read Database: ${bold}RDS (Postgres)${normal}" 
    "Eventstream: ${bold}Simple Queue Service${normal}, Event store: ${bold}DynamoDB${normal}, Read Database: ${bold}ElastiCache (Redis)${normal}"
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
select opt in "${options[@]}" "Quit"; do 

    case "$REPLY" in

    1 ) printf "\e[95m\nOption 1 selected\n${normal}Starting serverless... \n\n"; cd src/Lambda; sls deploy --config $SERVERLESS_CONFIGURATION_1; break;;
    2 ) printf "\e[95m\nOption 2 selected\n${normal}"; 
        cd src/Lambda; 
        printf "Building source code... \n\n"; 
        ./build.sh; 
        printf "\n\nStarting serverless... \n\n"; 
        sls deploy --config $SERVERLESS_CONFIGURATION_2; break;;

    $(( ${#options[@]}+1 )) ) echo "Goodbye!"; break;;
    *) echo "Invalid option. Try another one.";continue;;

    esac

done
