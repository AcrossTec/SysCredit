﻿using System.Xml.Linq;

namespace SysCredit.Api.Queries;


public class SQLite
{
    public static string FetchLoanType(long LoanTypeId)
    {
        return @$"
            SELECT * FROM LoanType
            WHERE IsDelete = 0;
                ";
    }

    public static string FetchLoanTypeById(long LoanTypeId)
    {
        return @$"
                SELECT *
                FROM LoanType
                WHERE IsDelete = 0 AND LoanTypeId = [LoanTypeId];
        ";
    }

    public static string FetchLoanTypeByName(string Name)
    {
        return @$"
                    SELECT T.*
                    FROM LoanType AS T
                    WHERE T.IsDelete = 0 AND T.Name = {Name};

        ";
    }


    public static string InsertLoanType(string Name)
    {
        return @$"

                INSERT INTO LoanType 
                    (
                        Name
                    )
                VALUES 
                    (
                        {Name}
                    );
                SELECT last_insert_rowid() AS LoanTypeId;
        ";
    }

    public static string UpdateLoanType(long LoanTypeId,string Name)
    {
        return @$"
            UPDATE LoanType
            SET
            Name = {Name},
            IsEdit = 1,
            ModifiedDate = CURRENT_TIMESTAMP 
            WHERE
            LoanTypeId = {LoanTypeId};

        ";
    }

    public static string DeleteLoanType(long LoanTypeId)
    {
        return @$"
                UPDATE LoanType
                SET
                IsDelete = 1,                      
                IsEdit = 1,                        
                ModifiedDate = CURRENT_TIMESTAMP,  
                DeletedDate = CURRENT_TIMESTAMP    
                WHERE
                LoanTypeId = ?;  
                ";
    }

    public static string FetchPaymentFrequency(long PaymentFrequency)
    {
        return @$"
            SELECT * FROM PaymentFrequency
            WHERE IsDelete = 0;
                ";
    }

    public static string FetchPaymentFrequencyById(long PaymentFrequencyId)
    {
        return @$"
                SELECT *
                FROM PaymentFrequency
                WHERE IsDelete = 0 AND PaymentFrequencyId = [PaymentFrequencyId];
        ";
    }

    public static string FetchPaymentFrequencyByName(string Name)
    {
        return @$"
                    SELECT PF.*
                    FROM PaymentFrequency AS PF
                    WHERE PF.IsDelete = 0 AND PF.Name = {Name};

        ";
    }

    public static string InsertPaymentFrequency(string Name)
    {
        return @$"

                INSERT INTO PaymentFrequency 
                    (
                        Name
                    )
                VALUES 
                    (
                        {Name}
                    );
                SELECT last_insert_rowid() AS PaymentFrquencyId;
        
        ";
    }

    public static string UpdatePaymentFrequency(long PaymentFrequencyId, string Name)
    {
        return @$"
            UPDATE PaymentFrequency
            SET
            Name = {Name},
            IsEdit = 1,
            ModifiedDate = CURRENT_TIMESTAMP 
            WHERE
            PaymentFrequencyId = {PaymentFrequencyId};

        ";
    }

    public static string DeletePaymentFrequency(long PaymentFrequencyId)
    {
        return @$"
                UPDATE PaymentFrequency
                SET
                IsDelete = 1,                      
                IsEdit = 1,                       
                ModifiedDate = CURRENT_TIMESTAMP,  
                DeletedDate = CURRENT_TIMESTAMP    
                WHERE
                PayementFrequencyId = ?;  
        ";
    }
}