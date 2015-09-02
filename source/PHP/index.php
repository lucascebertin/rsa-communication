<?php
	ini_set('display_startup_errors',1);
	ini_set('display_errors',1);
	error_reporting(-1);
	
	include('Crypt/RSA.php');

	echo "inicio";
    $XMLpublicKey = "publickey.net.xml";
    $XMLprivateKey = "privatekey.net.xml";
	
	$xml_text = file_get_contents($XMLpublicKey);
	$rsaclient = new Crypt_RSA();
	$xml = $rsaclient->loadKey($xml_text, CRYPT_RSA_PUBLIC_FORMAT_XML);
	$encrypted_bytes = $rsaclient->encrypt("Hello client");
	$encrypted = base64_encode($encrypted_bytes)."<br />";
		
	echo "<br>";
	echo "encrypted: " . $encrypted;
	echo "<br>";
	
	$xml_text = file_get_contents($XMLprivateKey);
	$rsaserver = new Crypt_RSA();
	$rsaserver->setPassword("123456");
	$rsaserver->loadKey($xml_text, CRYPT_RSA_PRIVATE_FORMAT_XML);
	$decrypted = $rsaserver->decrypt($encrypted_bytes);
	
	echo "decrypted: " . $decrypted . "<br />";
		
	$csharp_encrypted = "eu0RDpyAy7SQtZ1S2qMjcusvfYkkTouCZyIbtAh6zPzH6o76uR5sfoBv5KVV5GnxhuPtVwRoOnzrUGpPIlkUJ7BYsIL1+9Qo6AsgZ2IWDRLfniimE4/+Tj7rsEvo+if54pNv8FzxAZuoZ3HQWR1wWMrXsgkU1gCxwpumIy9BGCE=";
	$rsaserver2 = new Crypt_RSA();
	$rsaserver2->setPassword("123456");
	$rsaserver2->loadKey($xml_text, CRYPT_RSA_PRIVATE_FORMAT_XML);
	
	//echo base64_decode($csharp_encrypted);
	$decrypted = $rsaserver2->decrypt(base64_decode($csharp_encrypted));
		
	echo "c# decrypted: " . $decrypted . "<br />";
	
	echo "fim";
