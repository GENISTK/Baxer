<?php

    /*
    * Name: Connect
    * Author: Keagan Lidwell
    * Description: PHP script to easily connect and perform CRUD operations with AWS database.
    *
    * Notes: There is currently no security. In the future we should develop some sort of request validation process to make sure that
    *        only our app can call these functions. One idea to do this would to come up with an authorization string that requires 
    *        datetime, clientid, secretkey, appversion, etc. 
    */
    header('Content-type: application/json');
    try {

        // Login with user
        if(isset($_GET["username"])) {
            returnDataInJSON(FakeLogin($_GET["username"]));
        }    

    } catch(PDOException $e) {
        echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
    }

    // Connect to DB
    function connectToDb() {
        $hostname = 'baxtrax.ctooqvksmejc.us-east-2.rds.amazonaws.com';
        $username = 'KATAdminWEI';
        $password = 'PawsitiveThinking1';
        $database = 'KATABASE';

        $dbh = new PDO('mysql:host='. $hostname .';port=3306;dbname='. $database, $username, $password);
        return $dbh;
    }

    // Check if passed user is in the utable
    function FakeLogin($user) {
        $dbh = connectToDb();
        $query = "select * from utable where uname = ". $user .";";
        $sql = $dbh->prepare($query);
        $sql->execute();
        return $sql->fetchAll();
    }

    // List of tables in DB
    function showTables() {
        $dbh = connectToDb();

        $query = "show tables;";
        $sql = $dbh->prepare($query);
        $sql->execute();
        return $sql->fetchAll();
    }

    // List of columns in given table
    function getColumns($table) {
        $dbh = connectToDb();

        $query = "describe ".$table.";";
        $sql = $dbh->prepare($query);
        $sql->execute();
        return $sql->fetchAll();
    }

    // List of users in utable
    function getUsers() {
        $dbh = connectToDb();
        $query = "select * from utable";
        $sql = $dbh->prepare($query);
        $sql->execute();
        return $sql->fetchAll();
    }

    // Return in JSON format. Note: there are several different ways to format the JSON. Look at http://php.net/manual/en/pdostatement.fetchall.php for documentation.
    function returnDataInJSON($array) {
        echo json_encode($array);
    }

    // Return as echo. No longer needed but will keep around for testing purposes. 
    function returnData($data, $num_cols) {
        $first = true;
        echo '[ ';
        $num_rows = count($data);
        $curr_row = 0;
        foreach($data as $row) {
            echo '{ ';
            for($i = 0; $i < $num_cols; $i++) {
                if ($i == 0) {
                    echo $row[$i];
                } else {
                    echo ', ' . $row[$i];
                }
            }
            if(++$curr_row == $num_rows) {
                echo ' }';
            } else {
                echo ' }, ';
            }
        }
        echo ' ]';
    }
?>