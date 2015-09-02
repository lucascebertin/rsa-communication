openssl genrsa -out private.pem 1024
openssl rsa -in private.pem -out public.pem -pubout
openssl req -nodes -x509 -days 3650 -subj '/CN=www.example.com/emailAddress=info@example.com' -new -key private.pem -out certificate.crt
openssl pkcs12 -export -out certificate.pfx -inkey private.pem -in certificate.crt	-password pass:123456