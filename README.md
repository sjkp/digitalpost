https://digitaliser.dk/Media/638336537871668450/Digital%20Post%20-%20Technical%20Integration%20v1.41.pdf

https://test.digitalpost.dk/

Prod
[https://api.digitalpost.dk/](https://api.digitalpost.dk/apis/v1)



https://bitbucket.org/nc-dp/reference-systems-for-dotnet/src/master/


# Authentication> 

curl -v \
--http1.1 \
--key "my_foces_certificate.pkcs8" \
--key-type pem \
--cert-type pem \
--cert "my_foces_certificate.cer" \
-H "Authorization: Basic
ZTU0NWIzMzktODU0Mi00YTMwLWFlNzUtYzY3ZTRkMmE3Yjk5OjE1NjMyYThkLTQwN2MtNGMzYS1iN2IxLWFlY
mFhNTE0ZmNhYg==" \
"https://api.test.digitalpost.dk/apis/v1/contacts/"
