# rsa-communication
Comunicação entre uma aplicação C# e PHP utilizando RSA

Para criar o par assimetrico de chaves será necessário instalar o software OpenSSL.
Windows: http://gnuwin32.sourceforge.net/packages/openssl.htm
Linux: https://www.openssl.org/source/

Após a instalação, utilize o script .sh (de preferencia no Bash MINGW32 ou linux) para criar o par de chaves inicial,  rode a aplicação C# para criar arquivos XML necessários. (publickey.net.xml e privatekey.net.xml)

Copie estes dois para a pasta do PHP, modifique o conteúdo do index.php para decryptar o texto gerado pelo C# Console Application.

Agora utilize como base para criar o seu próprio mecanismo de comunicação e troca de mensagens de forma segura.
