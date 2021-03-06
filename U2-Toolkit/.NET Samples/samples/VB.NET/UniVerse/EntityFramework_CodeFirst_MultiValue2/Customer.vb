﻿Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions

Namespace EntityFramework_CodeFirst_MultiValue2

    Public Class Customer
        Private Sub New()
        End Sub

        <Key()> _
        <Column("CUSTID")> _
        Public Property ID() As Integer
            Get
                Return m_ID
            End Get
            Set(value As Integer)
                m_ID = Value
            End Set
        End Property
        Private m_ID As Integer

        'Single Value
        <Column("FNAME")> _
        Public Property FirstName() As String
            Get
                Return m_FirstName
            End Get
            Set(value As String)
                m_FirstName = Value
            End Set
        End Property
        Private m_FirstName As String

        'Single Value
        <Column("LNAME")> _
        Public Property LastName() As String
            Get
                Return m_LastName
            End Get
            Set(value As String)
                m_LastName = Value
            End Set
        End Property
        Private m_LastName As String

        Public Overridable Property Orders() As Order
            Get
                Return m_Orders
            End Get
            Set(value As Order)
                m_Orders = Value
            End Set
        End Property
        Private m_Orders As Order

    End Class

    Public Class Order
        Private Sub New()
        End Sub

        <Key()> _
        <Column("CUSTID")> _
        Public Property ID() As Integer
            Get
                Return m_ID
            End Get
            Set(value As Integer)
                m_ID = Value
            End Set
        End Property
        Private m_ID As Integer

        'Multi Value
        <Column("PRICE")> _
        Public Property Price() As Integer
            Get
                Return m_Price
            End Get
            Set(value As Integer)
                m_Price = value
            End Set
        End Property
        Private m_Price As Integer

        'Multi Value
        <Column("BUY_DATE")> _
        Public Property BuyDate() As DateTime
            Get
                Return m_BuyDate
            End Get
            Set(value As DateTime)
                m_BuyDate = Value
            End Set
        End Property
        Private m_BuyDate As DateTime

    End Class

    Public Class CustomerContext
        Inherits DbContext

        Public Sub New()
        End Sub

        Public Property Customers() As DbSet(Of Customer)
            Get
                Return m_Customers
            End Get
            Set(value As DbSet(Of Customer))
                m_Customers = Value
            End Set
        End Property
        Private m_Customers As DbSet(Of Customer)

        Public Property Orders() As DbSet(Of Order)
            Get
                Return m_Orders
            End Get
            Set(value As DbSet(Of Order))
                m_Orders = Value
            End Set
        End Property
        Private m_Orders As DbSet(Of Order)

        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)
            modelBuilder.Conventions.Remove(Of PluralizingTableNameConvention)()
            modelBuilder.Entity(Of Customer)().HasRequired(Function(c) c.Orders).WithRequiredPrincipal()
            modelBuilder.Entity(Of Customer)().ToTable("CUSTOMER")
            modelBuilder.Entity(Of Order)().ToTable("CUSTOMER")
        End Sub

    End Class

End Namespace
