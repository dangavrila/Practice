package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"os"
)

func helloHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	if r.Method == "GET" {
		w.Write([]byte("hello world"))
	} else {
		body, _ := ioutil.ReadAll(r.Body)
		w.Write(body)
	}
}

type InvokeRequest struct {
	Data     map[string]json.RawMessage
	Metadata map[string]interface{}
}

func handleQueueTrigger(w http.ResponseWriter, r *http.Request) {
	var invokeRequest InvokeRequest

	d := json.NewDecoder(r.Body)
	d.Decode(&invokeRequest)

	var parsedMessage string
	json.Unmarshal(invokeRequest.Data["queueMess"], &parsedMessage)
	fmt.Println(parsedMessage)
}

func main() {
	customHandlerPort, exists := os.LookupEnv("FUNCTIONS_CUSTOMHANDLER_PORT")
	if !exists {
		customHandlerPort = "8080"
	}
	mux := http.NewServeMux()
	mux.HandleFunc("/api/ServerlessGo", helloHandler)
	mux.HandleFunc("/QueueTriggerFunc", handleQueueTrigger)
	fmt.Println("Go server Listening on: ", customHandlerPort)
	log.Fatal(http.ListenAndServe(":"+customHandlerPort, mux))
}
