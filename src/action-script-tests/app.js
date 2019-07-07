let baseUrl = 'https://auth0.helloserverless.com';

baseUrl = 'http://localhost:58791/'; // test


function login(email, password, callback) {
    const request = require('request');
  
    request.get({
      url: `${baseUrl}/user/getInfo`,
      auth: {
        username: email,
        password: password
      }
      //for more options check:
      //https://github.com/mikeal/request#requestoptions-callback
    }, function(err, response, body) {
      if (err) return callback(err);
      if (response.statusCode === 401) return callback();
      const user = JSON.parse(body);
  
      callback(null, {
        user_id: user.user_id.toString(),
        nickname: user.nickname,
        email: user.email
      });
    });
}

login('1@1.com', 'p', (err, userObj) =>
{
  if(err)
  {
    console.error(err);
  }
  
  if(userObj === undefined)
  {
    return;
  }
});