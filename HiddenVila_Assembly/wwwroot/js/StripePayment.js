redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51KEaREKD8TkjV26mUJWqQVUqsX1IaES87uMW0DlGjpYSHWR48jzCVqubriHqVkRSIwew1j3bcXxtKKvgvEgWeRAc00j45iYnxU');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};