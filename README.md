<h1>Circuit breaker</h1>
The Circuit Breaker Pattern is a concept used in electrical circuits as well as in software design. In electrical circuits, a module known as a "circuit breaker" is responsible for interrupting the flow of electrical current under certain conditions, such as when there is an overload or a short circuit in the circuit. Essentially, the role of this module is to automatically disconnect the circuit in faulty conditions.

In the world of software, there is also a design pattern called the "Circuit Breaker Pattern" that is used for software development. This pattern is used to identify and prevent errors in external requests. It provides a logical mechanism to prevent continuous error repetition during times when a service is called and is in a maintenance state, experiences temporary failures, or encounters sudden network errors.

One of the significant differences between in-program requests and sending requests to external services is that requests sent to an external service can fail or remain unanswered for various reasons. This becomes particularly problematic when a large number of requests are made to an unresponsive external service, potentially overloading the system's resources and causing a widespread issue in the software.

The Circuit Breaker Pattern acts as an intermediary, preferably asynchronous, between the requester (client) and the responder (Service or Supplier). When errors reach a certain threshold, it activates the circuit-breaking mode, and no more requests are sent directly to the target service. Instead, this pattern handles sending an appropriate response to users based on the implemented logic.

For example, imagine in your software architecture you are using an external search service. If, for any reason, there is a problem with this search service that results in slow or no responses, and if the volume of requests sent to this service is high, it can pose a serious problem for your software. In this scenario, the Circuit Breaker Pattern can help by cutting off communication between the software and the target service, mitigating the traffic generated, and sending an appropriate response to users based on the implemented logic.


![image](https://github.com/amirfoad/PollyCircuitBreaker/assets/55051145/1ad7d7f5-a1b1-4abe-b5b2-0a32fcb3f944)


The structure of the Circuit Breaker design pattern 1-1:

Review of Figure 1-1:

In the first and second stages, the response to the request encounters a problem while returning from the supplier, and an error is displayed to the user. In the third stage, with repeated requests sent to the supplier and reaching the threshold for encountering errors, the circuit breaker becomes active. Finally, in the fourth stage, when the request is sent to the supplier by the circuit breaker, it is disconnected, and it also sends a response to the user based on the implemented logic.

<h2>Types of states in the Circuit Breaker design pattern:</h2>

<h3>Closed State:</h3>
In this state, the software communicates with the external service without any issues, and the program operates as expected.

<h3>Open State:</h3>
In this state, the number of error requests exceeds a predefined threshold within a specified time frame. Requests sent to the external service are blocked for a defined period, and the software, following the implemented scenario, responds to users in a circuit-breaking mode.

<h3>Half-Open State:</h3>
In this state, the software attempts to establish a request to the external service that had previously encountered errors.

When the specified time period elapses, the state changes to the Half-Open state. In this state, the software attempts to send a request to the external service. Two scenarios can occur:

If a request is sent to the external service and communication is successful, the retry counter (failed attempts) is reset to zero, and the design pattern transitions to the Closed state.

If, after sending a retry request, an error occurs, it is assumed that the problem still exists. Therefore, the timer resets, and the design pattern transitions back to the Open state (indicating that the external service is still unavailable).


![image](https://github.com/amirfoad/PollyCircuitBreaker/assets/55051145/dae87597-60ea-41da-ad94-507ebf21e19e)
